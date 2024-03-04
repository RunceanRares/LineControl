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
  $(document).ready(function () {

    $("form").kendoValidator();
    gridElement.data('kendoGrid') != undefined) {
      GridResize();
      $(window).resize(function () {
        GridResize();
      });
    }
})