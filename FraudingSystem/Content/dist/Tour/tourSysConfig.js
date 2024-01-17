
var tourSysConfig = new Tour({
})
function SysConfigTour() {
    $('#tourSysConfigImg').attr('src', "../Content/dist/img/tourguid.PNG");
    setCookies('setTourSysConfig' + userID, 'true');
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