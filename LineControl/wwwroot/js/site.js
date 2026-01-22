(function ($, window, document) {
  var gridElement = $('.s-gridContent');
  var htmlContent = $('html');
  var headerContent = $('.s-header-content');
  var pageTitle = $('.s-pageTitle');
  var footerContent = $('.sticky-footer');

  function GridResize() {
    gridElement.height(Math.floor(htmlContent[0].getBoundingClientRect().height) - headerContent.outerHeight() - pageTitle.outerHeight() - footerContent.outerHeight());
    gridElement.data('kendoGrid').resize();
  }

  window.getSearchParameters = function () {
    // Încercăm să luăm textul direct din inputul utilizatorului
    var inputField = $(".s-device-itemnumber").data("kendoComboBox").input;
    var text = inputField.val();

    return {
      itemNumber: text
    };
  };

  $(function () {

    // Verificăm dacă kendo este disponibil înainte de orice
    if (typeof kendo === 'undefined') {
      console.error("Kendo UI is not loaded yet! Logic might fail.");
    }


    $("form").kendoValidator();
    if (gridElement.length > 0 && gridElement.data('kendoGrid'))
    {
      GridResize();
      window.addEventListener('resize', function () {
        GridResize();
      });
    }

    $('.s-CancelBtn').on('click', function () {
      var url = $(this).data('request-url');
      window.location.href = url;
      return false;
    });    
    function getSearchParameters() {
      var combobox = $(".s-device-itemnumber").data("kendoComboBox");
      var text = "";

      if (combobox) {
        // Luăm ce a scris utilizatorul
        text = combobox.text();
      }

      // Returnăm obiectul cu numele parametrului exact ca în Controller: SearchItemNumbers(string itemNumber)
      return {
        itemNumber: text
      };
    }

    $(function () {
      var comboBox = $(".s-device-itemnumber").kendoComboBox({
        placeholder: "Type item number...",
        dataTextField: "ItemNumber",
        dataValueField: "ItemNumber",
        filter: "contains",
        minLength: 2,
        autoBind: false,
        suggest: true,
        dataSource: {
          transport: {
            read: {
              url: "/DeviceIssues/SearchItemNumbers",
              dataType: "json"
            },
            parameterMap: function (options, operation) {
              // Trimite parametrul itemNumber bazat pe filtrul aplicat
              if (operation === "read") {
                // Verifică dacă există filtrul aplicat și trimite valoarea
                if (options.filter && options.filter.filters.length > 0) {
                  return {
                    itemNumber: options.filter.filters[0].value
                  };
                }
                return {}; // dacă nu există filtr, nu trimite nimic
              }
            }
          },
          serverFiltering: true // asigură că filtrarea se face pe server
        }
      }).data("kendoComboBox");

      if (comboBox) {
        // ✅ VERIFICĂ PE CE PAGINĂ TE AFLI
        var isReservationPage = $(".s-device-reservation-form").length > 0;
        var isIssuePage = $(".s-device-issue-form").length > 0;

        comboBox.bind("select", function (e) {
          console.log("Eveniment select declanșat");

          // Apelează funcția corespunzătoare paginii
          var dataItem = this.dataItem(e.item.index());

          // Verificăm dacă am recuperat obiectul cu succes
          if (dataItem) {
            if (isReservationPage) {
              // Acum putem accesa proprietatea ItemNumber
              var selectedItemNumber = dataItem.ItemNumber;
              onItemNumberChangeReservation(selectedItemNumber);
            } else if (isIssuePage) {
              onItemNumberChange(e);
            }
          }
        });
      }

      // Eveniment la apăsarea tastei Enter
      if (comboBox) {
        // Adaugă doar funcționalitatea Enter
        $(".s-device-itemnumber").on("keydown", function (e) {
          if (e.key === "Enter") {
            e.preventDefault();
            var value = comboBox.text();
            comboBox.dataSource.filter({
              field: "ItemNumber",
              operator: "contains",
              value: value
            });
            comboBox.open();
          }
        });
      }

      $(".s-device-reservation-form").on("keydown", "input", function (e) {
        if (e.key === "Enter" && !$(this).is("input[type='submit']")) {
          e.preventDefault();
          console.log("ENTER pressed - prevented form submit");
          return false;
        }
      });
    });

    function deviceEdit(e) {
      e.preventDefault();

      var dataItem = this.dataItem($(e.currentTarget).closest('tr')),
        url = $(e.currentTarget).data('url');

      window.location.href = url.replace('__id__', dataItem.id);
    }

    function showReservationDetails(details) {
      var message = "Current Reservation:\n" +
        "Start Date: " + details.startDate + "\n" +
        "End Date: " + details.endDate + "\n" +
        "Reserved by: " + details.reservedBy;
      // Poți folosi un modal Kendo sau o altă metodă de afișare
      console.log(message);
    }

  });

  window.onCalibrationTesterDataBound = function (e) {
    var dropdown = e.sender;
    // Dacă nu există nicio valoare selectată, selectăm primul element (userul curent)
    if (!dropdown.value() && dropdown.dataSource.data().length > 0) {
      dropdown.select(0);
      dropdown.trigger("change"); // Anunțăm modelul că s-a schimbat valoarea
    }
  };

  window.onItemNumberChange = function (e) {
    var dataItem = e.dataItem;
    var selectedDeviceId = dataItem.Id;
    console.log("ID Dispozitiv selectat:", selectedDeviceId);
    if (!dataItem) {
      console.log("No dataItem selected");
      return;
    }

    var deviceIdInput = $("input[name='DeviceId']");
    deviceIdInput.val(dataItem.Id || 0);

    var issueIdInput = $("input[name='IssueId']");
    issueIdInput.val(dataItem.LastIssueId || 0);

    var treeList = $("#childrenTree").data("kendoTreeList");
    console.log(treeList);
    if (treeList) {
      treeList.dataSource.read({
        deviceId: selectedDeviceId
      });
    }

    $.get("/DeviceIssues/CheckDeviceIssuesStatus", { itemNumber: dataItem.ItemNumber })
      .done(function (response) {
        console.log("CheckDeviceIssuesStatus:", response);

        if (response.canRetrieve) {
          $("#saveIssue").hide();
          $("#saveReceive").show();
        } else {
          $("#saveIssue").show();
          $("#saveReceive").hide();
        }

        $("input[name='IsRetrieve']").val(response.canRetrieve);
      })
      .fail(function (xhr, status, error) {
        console.error("Error checking device status:", error);
      });

    if (dataItem && dataItem.ItemNumber) {
      kendo.ui.progress($("body"), true);

      var baseUrl = "/DeviceIssues/Issues";

      window.location.href = baseUrl + "?itemNumber=" + encodeURIComponent(dataItem.ItemNumber);
    }
  };


  window.onItemNumberChangeReservation = function (itemNumber) {
    console.log("=== onItemNumberChangeReservation called ===");

    var combobox = $("#ItemNumber").data("kendoComboBox");

    if (!combobox) {
      console.error("ERROR: ComboBox not found!");
      return;
    }

   // var itemNumber = combobox.value();
    console.log("Selected ItemNumber:", itemNumber);

    if (!itemNumber || itemNumber === "") {
      console.log("No item number selected");
      return;
    }
   
    console.log("Making AJAX call to /Reservation/CheckReservationStatus");

    $.ajax({
      url: "/Reservation/CheckReservationStatus",
      type: "GET",
      data: { itemNumber: itemNumber },
      success: function (response) {
        console.log("=== Response received ===", response);

        var deviceIsBlocked = response.hasActiveIssue || response.isReserved;
        var periodDropdown = $("#PeriodId").data("kendoDropDownList");
        var saveButton = $("#saveReserve").data("kendoButton");
        var deviceIsBlocked = response.hasActiveIssue || response.isReserved;
        console.log("=== Response deviceIsBlocked ===", deviceIsBlocked);

        // 3. Aplicăm starea pe DropDownList
        if (periodDropdown) {
          periodDropdown.enable(!deviceIsBlocked);
        }

        if (response.hasActiveIssue && response.redirectUrl) {

          if (confirm("Acest dispozitiv are un issue activ. Vei fi redirecționat către pagina de soluționare.")) {
            // AICI SE FACE REDIRECȚIONAREA EFECTIVĂ
            window.location.href = response.redirectUrl;
          }
          // Dacă utilizatorul dă Cancel, poți goli câmpul ItemNumber sau bloca butonul save
          else {
            var combobox = $("#ItemNumber").data("kendoComboBox");
            combobox.value(""); // Resetăm selecția
          }

          return; // Oprim execuția restului funcției ca să nu mai populăm formularul de rezervare
        }

        if (response.hasActiveReservation && response.redirectUrl) {
          console.log("Redirect to index");
          window.location.href = response.redirectUrl;
        }

        $(".s-device-reservation-deviceid").val(response.deviceId || 0);
        $("input[name='Manufacturer']").val(response.manufacturer || "");
        $("input[name='DeviceModel']").val(response.model || "");
        $("input[name='Designation']").val(response.designation || "");
        $(".s-device-reservation-isuniversal").val(response.isUniversal);
        $(".s-device-reservation-hasranges").val(response.hasRanges);
        $(".s-device-reservation-checkcalibration").val(response.checkCalibration);

        if (response.isReserved) {
          console.log("⚠️ Device is RESERVED!");
          var saveButton = $("#saveReserve").data("kendoButton");
          if (saveButton) {
            saveButton.enable(false);
          }

          var message = "⚠️ This device is already reserved!";

          if (response.reservationDetails) {
            message += "\n\n📅 Reservation Details:";
            message += "\n• Start Date: " + response.reservationDetails.startDate;
            message += "\n• End Date: " + response.reservationDetails.endDate;
            if (response.reservationDetails.reservedBy) {
              message += "\n• Reserved By: " + response.reservationDetails.reservedBy;
            }
          }

          alert(message);
        } else {
          console.log("✅ Device is available");

          var saveButton = $("#saveReserve").data("kendoButton");
          if (saveButton) {
            saveButton.enable(true);
          }
        }

        if (response.deviceId > 0) {
          var treeList = $("#childrenTree").data("kendoTreeList");
          if (treeList) {
            console.log("Refreshing TreeList");
            treeList.dataSource.read();
          }
        }
      },
      error: function (xhr, status, error) {
        console.error("=== AJAX Error ===");
        console.error("Status:", status);
        console.error("Error:", error);
        console.error("Response:", xhr.responseText);
        alert("Error checking device status: " + error);
      }
    });
  };

    window.reservation = {
    dataSourceError: function(e) {
      console.error("Reservation DataSource error:", e);
      if (e.errors) {
        var errorMsg = typeof e.errors === 'string' ? e.errors : JSON.stringify(e.errors);
        alert("Error loading data: " + errorMsg);
      }
    },
    hierarchyDataBound: function(e) {
      console.log("Hierarchy data bound successfully");
    }
  };

  // Validare form
  window.validateReservationForm = function() {
    var deviceId = $(".s-device-reservation-deviceid").val();
    if (!deviceId || deviceId === "0") {
      alert("Please select a valid device first!");
      return false;
    }
    return true;
  };

  window.openEditUser = function (e) {
    e.preventDefault();
    var tr = $(e.target).closest("tr"); //selecteaza cel mai apropiat element
    var data = this.dataItem(tr); //extrage datele randului
    var userId = data.Id; //Extrage id ul 
    var url = this.element.data('action-edit'); //extrage adresa url de editare(aici face referire la metoda din controller)
    window.location.href = url + "/" + userId; //actualizeaza locatia paginii
  }

  window.openEditActivity = function (e) {
    e.preventDefault();
    var tr = $(e.target).closest("tr"); //selecteaza cel mai apropiat element
    var data = this.dataItem(tr); //extrage datele randului
    var activityId = data.Id; //Extrage id ul 
    var url = this.element.data('action-edit'); //extrage adresa url de editare(aici face referire la metoda din controller)
    window.location.href = url + "/" + activityId; //actualizeaza locatia paginii
  }

  window.openEditDevice = function (e) {
    e.preventDefault();
    var tr = $(e.target).closest("tr"); //selecteaza cel mai apropiat element
    var data = this.dataItem(tr); //extrage datele randului
    var deviceId = data.Id; //Extrage id ul 
    var url = this.element.data('action-edit'); //extrage adresa url de editare(aici face referire la metoda din controller)
    window.location.href = url + "/" + deviceId; //actualizeaza locatia paginii
  }

  window.openEditDeviceMode = function (e) {
    e.preventDefault();
    var grid = $("#DeviceModeGrid").data("kendoGrid");
    var tr = $(e.target).closest("tr");
    var data = grid.dataItem(tr);
    var deviceModeId = data.Id;
    var url = grid.element.data('action-edit');
    window.location.href = url + "/" + deviceModeId;
  }

  window.openEditDeviceClassMode = function (e) {
    e.preventDefault();
    var grid = $("#DeviceClassModeGrid").data("kendoGrid");
    var tr = $(e.target).closest("tr");
    var data = grid.dataItem(tr);
    var deviceClassModeId = data.Id;
    var url = grid.element.data('action-edit');
    window.location.href = url + "/" + deviceClassModeId;
  }

  window.openEditCalibration = function (e) {
    e.preventDefault();
    var tr = $(e.target).closest("tr");
    var data = this.dataItem(tr);
    var deviceCalibrationId = data.Id;
    var url = this.element.data('action-edit');
    window.location.href = url + "/" + deviceCalibrationId;
  }

  window.openEditCompanyLocation = function (e) {
    e.preventDefault();
    var tr = $(e.target).closest('tr');
    var data = this.dataItem(tr);
    var companyLocationId = data.Id;
    var url = this.element.data('action-edit');
    window.location.href = url + "/" + companyLocationId;
  }

  window.onDeviceChange = function (e) {
    var selectedDeviceId = this.value();
    if (!selectedDeviceId) return;

    $.getJSON('/CalibrationOrder/GetDeviceCreatorByDeviceId',
      { deviceId: selectedDeviceId },
      function (data) {
        $("#CreatedBy").val(data.createdBy);
      });   

    $.getJSON('/CalibrationOrder/GetDeviceTestLocation',
      { deviceId: selectedDeviceId },
      function (data) {
        $("#TestLocation").val(data.testLocation);
      }
    );
  }

  window.integrateDevice = function (e) {
    var itemNumber = $("#IntegratedDevice").val();
    var container = $("#integrate-container");

    var parentId = container.data("parent-id");
    var integrateUrl = container.data("integrate-url");

    if (!itemNumber) {
      alert("Introduceți un număr de inventar (Item Number)!");
      return;
    }

    console.log("Încercare integrare la URL:", integrateUrl);
    $.ajax({
      url: integrateUrl,
      type: 'POST',
      data: {
        "device.ItemNumber": itemNumber,
        "device.ParentId": parentId
      },
      success: function (result) {
        var treeList = $("#childrenTree").data("kendoTreeList");

        if (treeList && result) {
          console.log("am intrat si aici");
          treeList.dataSource.read();
        } else {
          console.error("Kendo TreeList not found on #childrenTree");
        }

        $("#IntegratedDevice").val("");
        alert("Dispozitiv integrat cu succes!");
      },
      error: function (err) {
        alert("Error: " + err.responseText);
      }
    });
  };

  // Atașăm click folosind delegare (sigur chiar dacă butonul e recreat)
  $(document).on("click", ".s-device-integrate-button", function (e) {
    window.integrateDevice(e);
  });

})(jQuery, window, document);