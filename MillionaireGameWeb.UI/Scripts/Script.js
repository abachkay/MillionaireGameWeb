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

function showModal()
{    
    $('#modalEmail').openModal();    
}

function usePhone(to, description) {
    var emailTemplate = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    if (emailTemplate.test($('#email').val())) {         
        $("#modalEmail").closeModal();
        $.ajax({
            type: "POST",
            url: '/Home/UsePhone',
            data: { to: to, description: description },
            success: function (result) {
                $('#GameContainer').html(result);                
                Materialize.toast('Done.', 4000)
            },
        });        
    } else {
        $('#emailError').text('Email is invalid.');
    }    
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