﻿(function ($, window, document) {
  var gridElement = $('.s-gridContent');
  var htmlContent = $('html');
  var headerContent = $('.s-header-content');
  var pageTitle = $('.s-pageTitle');
  var footerContent = $('.sticky-footer');

  function GridResize() {
    gridElement.height(Math.floor(htmlContent[0].getBoundingClientRect().height) - headerContent.outerHeight() - pageTitle.outerHeight() - footerContent.outerHeight());
    gridElement.data('kendoGrid').resize();
  }

  $(document).ready(function () {

    $("form").kendoValidator();
    gridElement.data('kendoGrid') != undefined
    {
      GridResize();
      $(window).resize(function () {
        GridResize();
      });
    }

    $('.s-CancelBtn').click(function () {
      var url = $(this).data('request-url');
      window.location.href = url;
      return false;
    });
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
    var tr = $(e.target).closest("tr");
    var data = this.dataItem(tr);
    var deviceModeId = data.Id;
    var url = this.element.data('action-edit');
    window.location.href = url + "/" + deviceModeId;
  }

  window.openEditDeviceClassMode = function (e) {
    e.preventDefault();
    var tr = $(e.target).closest("tr");
    var data = this.dataItem(tr);
    var deviceClassModeId = data.Id;
    var url = this.element.data('action-edit');
    window.location.href = url + "/" + deviceClassModeId;
  }
})(jQuery, window, document);