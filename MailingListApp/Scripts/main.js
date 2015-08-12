$(document).ready(function () {

    GetMailingLists();
    GetSubscribers();

    $('#newsletter-sign-up').click(function (event) {
        var newsletterIDs = $("input:checkbox:checked").map(function () {
            return parseInt($(this).val(), 10);
        }).get();

        event.preventDefault();
        $.ajax({
            method: "POST",
            url: "/api/Subscribers",
            data: {
                "Email": $("#email").val(),
                "First": $("#firstname").val(),
                "MailingLists": newsletterIDs
            }
        }).done(function (msg) {
            alert("Data Saved: " + msg);
            $("#email").val('')
            $("#firstname").val('')
            GetMailingLists();
            GetSubscribers();
        });
    });

    function GetMailingLists() {
        $.ajax({
            url: "/api/MailingLists",
            data: {
                format: 'json'
            },
            success: function (data) {
                var mailingListCheckboxList = "";
                for (var i = 0; i < data.length; i++) {
                    console.log(data[i])
                    mailingListCheckboxList = mailingListCheckboxList + data[i].Name + " <input type='checkbox' value='" + data[i].Id + "'/><br>";
                }
                $('#mailing-lists-signup').html(mailingListCheckboxList)
                $('#mailing-lists').text(JSON.stringify(data));
                $('#notification-bar').text('Page successfully loaded');
            },
            error: function () {
                $('#notification-bar').text('Error');
            }
        });
    }

    function GetSubscribers() {
        $.ajax({
            url: "/api/Subscribers",
            data: {
                format: 'json'
            },
            success: function (data) {

                $('#subscribers-list').text(JSON.stringify(data));
                $('#notification-bar').text('Page successfully loaded');
            },
            error: function () {
                $('#notification-bar').text('Error');
            }
        });
    }
});