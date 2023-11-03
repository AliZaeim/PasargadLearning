function validateEmail(email) {
    var re = /[A-Z0-9._%+-]+\\@[A-Z0-9.-]+\.[A-Z]{2,4}/igm;
    if (re.test(email)) {
        return true;
    } else {
        return false;
    }
}
function IsEmail(email) {
    var regex = /^([a-zA-Z0-9_\\.\\-\\+])+\\@(([a-zA-Z0-9\\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (!regex.test(email)) {
        return false;
    } else {
        return true;
    }
}
$(function () {
    var url = window.location.href;
    $("#navigation li").removeClass("active");
    $("#navigation li a").each(function () {
        if (url === (this.href)) {
            $(this).parent().closest('li').addClass('active');
            $(this).parent().parent().closest('li').addClass('active');
        }
    });
});    
$(document).ajaxStart(function () {
    $('#LayProgress').css("display", "block");
});
$(document).ajaxComplete(function () {

    $('#LayProgress').css("display", "none");
});
$(document).ready(function () {
    var url = window.location.href;
    $("html, body").animate({ scrollTop: 0 }, "slow");
    $("#navigation li a").each(function () {       
        if (url === $(this).href || url === $(this).attr("data-url")) {            
            $("#navigation li").removeClass("active");
            $(this).parent().closest('li').addClass('active');
        }
    });   
    var windowH = $(window).height() / 2;
    $(window).on('scroll', function () {
        if ($(this).scrollTop() > windowH) {
            $("#myBtn").css('display', 'flex');
        } else {
            $("#myBtn").css('display', 'none');
        }
    });
    $('#myBtn').on("click", function () {
        $('html, body').animate({ scrollTop: 0 }, 300);
    });
    $("#btnSendEmail").click(function () {
        var em = $("#txtmail").val();

        if (em === "") {
            Msg.error("ایمیل را وارد کنید", 2000);
            return false;
        }
       
        if (!IsEmail(em)) {
            Msg.warning("ایمیل نامعتبر است !", 2000);
            return false;
        }
        $.ajax({
            url: "/Home/GetEmail",
            data: { email: em },
            type: "POST",
            success: function (result) {
                Msg.warning(result);
                $("#txtmail").val('');
            },
            error: function () {
                Msg.error('ثبت ایمیل امکان پذیر نیست');
            }
        });
    });
    $(document).find(".btnReginCourse").on("click", function () {
        alert("red");
    });
});