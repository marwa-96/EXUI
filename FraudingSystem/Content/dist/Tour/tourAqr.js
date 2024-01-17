
var tourAqr = new Tour({
})
function AqrTour() {
    $('#tourAqrImg').attr('src', "../Content/dist/img/tourguid.PNG");
    setCookies('setTourAqr' + userID, 'true');
    tour.end();
    tourAtt.end();
    tourPM.end();
    tourTimeSheet.end();
    tourDashboardAtt.end();
    tourEmployeeDashboard.end();
    tourCRM.end();
    tourHR.init();
    tourHR.restart();
}