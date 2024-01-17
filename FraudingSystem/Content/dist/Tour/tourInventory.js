
var tourInv = new Tour({
})
function InvTour() {
    $('#tourInvImg').attr('src', "../Content/dist/img/tourguid.PNG");
    setCookies('setTourInv' + userID, 'true');
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