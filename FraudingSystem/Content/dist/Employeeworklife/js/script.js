(function($){
        $(window).load(function(){
            $(".sidebar-body").mCustomScrollbar({
            	theme:"light-thin",
            	scrollInertia: 400,
            	autoHideScrollbar: true,
            	mouseWheel:{ preventDefault: true }
            });
        });
})(jQuery);


$(function(){

  $("div.holder").jPages({
    containerID : "itemContainer",
    perPage : 4,
    previous : "",
    next : "",
    keyBrowse : true
  });

});


$( "#filterStart, #filterEnd, #dateStart, #dateEnd, #leaveDate_from, #leaveDate_to" ).dateDropper();

$('#timeStart, #timeEnd').clockpicker({
    placement: 'top',
    align: 'left',
    autoclose: true
});