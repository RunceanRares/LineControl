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

  $(function () {
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

    function deviceEdit(e) {
      e.preventDefault();

      var dataItem = this.dataItem($(e.currentTarget).closest('tr')),
        url = $(e.currentTarget).data('url');

      window.location.href = url.replace('__id__', dataItem.id);
    }
  });

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

  window.integrateDevice = function (e) {
    e.preventDefault();
    console.log("Butonul de integrare a fost apăsat.");
    var $device = $('.s-device-integrate');
    const itemNumber = $device.val().trim();//elimina spatiile goale
    const dataSource = $("#childrenGrid").data("kendoGrid").dataSource;
    const $button = $('.s-device-integrate-button');

    if (!itemNumber) {
      showErrorMessage($device.data('required') || "Item number is required!");
      toggleInvalidClass($device, false);
      return;
    }

    //dezactiveaza butonul
    const total = dataSource.data().length;
    const model = dataSource.insert(total, { ItemNumber: itemNumber });

    const onSuccess = () => {
      $device.val(''); // Golește câmpul de input
      dataSource.unbind('sync', onSuccess);
      dataSource.unbind('error', onError);
    };

    const onError = (args) => {
      hideMessages();
      if (args.errors) {
        for (const error in args.errors) {
          if (args.errors.hasOwnProperty(error)) {
            showErrorMessage(args.errors[error].errors[0]);
          }
        }
      }
      dataSource.cancelChanges(model); // Revocă modificările în caz de eroare
      dataSource.unbind('sync', onSuccess);
      dataSource.unbind('error', onError);
    };
    dataSource.bind('sync', onSuccess);
    dataSource.bind('error', onError);
    dataSource.sync();
  }
})(jQuery, window, document);