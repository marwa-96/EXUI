
// Start Tour Employee Attendance
function startTourAtt() {
    $('#TourAttImg').attr('src', "../Content/dist/img/tourguid.PNG");
    setCookies('setTourAtt' + userID, 'true');
    tour.end();
    tourDashboardAtt.end();
    tourEmployeeDashboard.end()
    tourPM.end();
    tourTimeSheet.end();
    tourCRM.end();
    tourAtt.init();
    tourAtt.restart();
}
var tourAtt = new Tour({
    steps: [
        {
            element: ".fc-view-container",
            title: LocalResource('AttHelp_CalenderTitle'),
            content: LocalResource('AttHelp_CalenderContent'),
            placement: "top"
        },
    {
        element: "#colorGuidHelp",
        title: LocalResource('AttHelp_ColorGuidTitle'),
        content: LocalResource('AttHelp_ColorGuidContent'),
        placement: "top"

    },
    {
        element: "#draggableRequests",
        title: LocalResource('AttHelp_DraggableRequestTitle'),
        content: LocalResource('AttHelp_DraggableRequesContent') + "<img src='../../Content/dist/img/draganddrop.png' style='width: 70px;' class='center-block'/>",
        placement: "top"


    }
    ,
    {
        element: "#showRequest",
        title: LocalResource('AttHelp_ShowMyRequestTitle'),
        content: LocalResource('AttHelp_ShowMyRequestContent'),
        placement: "top"


    }
    ,
    {
        element: "#showFowllowers",
        title: LocalResource('AttHelp_ShowFollowersRequestTitle'),
        content: LocalResource('AttHelp_ShowFollowersRequestContent'),
        placement: "top"


    },

    {
        element: ".fc-left",
        title: LocalResource('AttHelp_CurrentMonthTitle'),
        content: LocalResource('AttHelp_CurrentMonthContent'),
        placement: "top"


    }
    ,
    {
        element: ".selectEmployeeHelp",
        title: LocalResource('AttHelp_ChangeEmployeeTitle'),
        content: LocalResource('AttHelp_ChangeEmployeeContent'),
        placement: "top"


    }

    ]
});
// End Tour Employee Attendance

//dashboard tour att

function startTourDashboardAtt() {
    $('#TourDashbaordAttImg').attr('src', "../Content/dist/img/tourguid.PNG");
    setCookies('setTourDashboardAtt' + userID, 'true');
    tour.end();
    tourAtt.end();
    tourPM.end();
    tourTimeSheet.end();
    tourEmployeeDashboard.end();
    tourCRM.end();
    tourDashboardAtt.init();
    tourDashboardAtt.restart();

}

var tourDashboardAtt = new Tour({
    steps: [
        {
            element: "#attWorkingHours",
            title: "",
            content: LocalResource('AttHelp_NoOfWorkingHoursForyear'),
            placement: "top"
        },
         {
             element: "#attAbssanceVSVacations",
             title: "",
             content: LocalResource('AttHelp_AbsenceVsVacation'),
             placement: "top"
         },
          {
              element: "#attOverTimeVsDelay",
              title: "",
              content: LocalResource('AttHelp_OverTimeVsDelay'),
              placement: "top"
          },
           {
               element: "#attDelayVsPenality",
               title: "",
               content: LocalResource('AttHelp_DelayVsPenality'),
               placement: "top"
           },
            {
                element: "#attAttendanceRuler",
                title: "",
                content: LocalResource('AttHelp_AttendanceRuler'),
                placement: "top"
            }
    ]

})