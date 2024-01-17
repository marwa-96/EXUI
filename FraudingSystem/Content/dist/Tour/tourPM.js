
//Start Tour PM Dashboard
function startTourPM() {
    $('#TourPmImg').attr('src', "../Content/dist/img/tourguid.PNG");
    setCookies('setTourPM' + userID, 'true');
    tour.end();
    tourAtt.end();
    tourTimeSheet.end();
    tourDashboardAtt.end();
    tourEmployeeDashboard.end();
    tourCRM.end();
    tourPM.init();
    tourPM.restart();

}
var tourPM = new Tour({
    steps: [
    {
        element: ".task-timer",
        title: "Current Task",
        content: "Current task timer showing the number of minutes.",
        placement: "top"
    },
    {
        element: ".reasons-panel",
        title: "Shortcuts",
        content: "Shortcuts for the quick admin frequently tasks",
        placement: "top"

    },
    {
        element: "#activeTask",
        title: "Activites",
        content: "<img src='../../Content/dist/img/taskone.PNG' style='width:100%'/> Here you will find the last of tasks assigned to you. you can start ,pause and show task details in attachment by clicking on the task title",
        placement: "top",
        template: '<div class="popover" role="tooltip" style="max-width: 400px !important;"> <div class="arrow"></div> <h3 class="popover-title"></h3> <div class="popover-content"></div> <div class="popover-navigation"> <div class="btn-group nextPrev"> <button class="btn btn-sm btn-default" data-role="prev">' + LocalResource("General_Prev") + '</button> <button class="btn btn-sm btn-default" data-role="next">' + LocalResource("General_Next") + '</button> <button class="btn btn-sm btn-default" data-role="pause-resume" data-pause-text="Pause" data-resume-text="Resume">Pause</button> </div> <button class="btn btn-sm btn-default endTour" data-role="end">' + LocalResource("General_EndTour") + '</button> </div> </div>',


    }
    ]

});
//End Tour PM Dashboard

//Start Tour PM TimeSheet
function startTourTimeSheet() {
    $('#TourTimeSheetImg').attr('src', "../Content/dist/img/tourguid.PNG");
    setCookies('setTourTimeSheet' + userID, 'true');
    tour.end();
    tourAtt.end();
    tourPM.end();
    tourDashboardAtt.end();
    tourEmployeeDashboard.end();
    tourTimeSheet.init();
    tourTimeSheet.restart();
}
var tourTimeSheet = new Tour({
    steps: [
    {
        element: ".selectEmployeeTimeSheet",
        title: "Employees",
        content: "You can select your name for the time sheet from here or you can register time sheet for one of your team.",
        placement: "top"
    },
    {
        element: ".showWeekTasks",
        title: "Week",
        content: "Select the week you want to register timesheet on. Click show to show your time sheet.",
        placement: "top"
    },
    {
        element: "#TimeSheetGrid",
        title: "Timesheet",
        content: "This is the number of working hours Based on your attendance.",
        placement: "top"
    },
     {
         element: ".addTimeSheetBtn",
         title: "Add",
         content: "Here you can add your timesheet keeping in mind you can't add two rows for the same week just one row a week.",
         placement: "top"
     }
     ,
     {
         element: "#ShowComents",
         title: "Show comments",
         content: "Here you can write / show comment on the current time sheet.",
         placement: "top"
     }
     //,
     //{
     //    element: "#SubmitTimeSheet",
     //    title: "Submit TimeSheet",
     //    content: "After finishing your weekly time sheet you can submit timesheet for here.",
     //    placement: "top"
     //}
    ]

});

//End Tour PM TimeSheet
