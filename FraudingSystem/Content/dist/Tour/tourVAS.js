
var tourVAS = new Tour({
})
function VASTour() {
    $('#tourVASImg').attr('src', "../Content/dist/img/tourguid.PNG");
    setCookies('setTourVAS' + userID, 'true');
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