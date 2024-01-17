
var tourEvocher = new Tour({
})
function EvocherTour() {
    $('#tourEvocherImg').attr('src', "../Content/dist/img/tourguid.PNG");
    setCookies('setTourEvocher' + userID, 'true');
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