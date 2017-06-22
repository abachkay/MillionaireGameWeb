$(document).ready(function () {
    $.ajax({
        type: "POST",
        url: "/Home/GetGameElements",        
        success: function (result) {
            $('#GameContainer').html(result);            
        },        
    });
})

function answer(index) {    
    $.ajax({
        type: "POST",
        url: '/Home/CheckAnswer',
        data: { answerIndex: index },
        success: function (result) {
            $('#GameContainer').html(result);            
        },
    });
    
}

function use50_50(description) {
   
    $.ajax({
        type: "POST",
        url: '/Home/Use50_50',
        data: { description: description },
        success: function (result) {
            $('#GameContainer').html(result);
        },
    });
}

function usePhone(from,to,description) {
    $.ajax({
        type: "POST",
        url: '/Home/UsePhone',
        data: { from: from, to:to, description: description },
        success: function (result) {
            $('#GameContainer').html(result);
        },
    });
}

function usePeople(description) {
    var win = window.open('http://google.com?q=' + description, '_blank');
    win.focus();
    $.ajax({
        type: "POST",
        url: '/Home/UsePeople',
        data: { description: description },
        success: function (result) {
            $('#GameContainer').html(result);
        },
    });      
}