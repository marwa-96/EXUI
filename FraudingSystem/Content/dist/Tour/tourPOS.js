
var tourPOS = new Tour({
})
function AqrTour() {
    $('#tourPOSImg').attr('src', "../Content/dist/img/tourguid.PNG");
    setCookies('setTourPOS' + userID, 'true');
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