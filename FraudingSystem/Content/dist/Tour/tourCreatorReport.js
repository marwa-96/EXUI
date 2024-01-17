
var tourCreatorReport = new Tour({
})
function CreatorReportTour() {
    $('#tourCreatorReportImg').attr('src', "../Content/dist/img/tourguid.PNG");
    setCookies('setTourCreatorRepor' + userID, 'true');
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