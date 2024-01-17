
var tourAcc = new Tour({
})
function AccTour() {
    $('#tourAccImg').attr('src', "../Content/dist/img/tourguid.PNG");
    setCookies('setTourAcc' + userID, 'true');
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