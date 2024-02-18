$(document).ready(function () {
    $(".addBasketBtn").click(function () {
        let id = $(this).data("id");

        $.ajax({
            method: "POST",
            url: "/basket/add",
            data: {
                id:id
            },
            success: () => {
                console.log("OK")
            }

        })

    })

    $(".removeBtn").click(      
        function () {
            let id = $(this).data("id");
            $.ajax({
                method: "GET",
                url: "/basket/delete",
                data: {
                    id: id 
                },
                success: () => {
                    $(`.basketproducts[id=${id}]`).remove();
                }


            })
        }
    )
});



$(document).ready(function () {
    $(".btn-message").click(function (e) {
        e.preventDefault();

        let name = $("#name").val();
        let email = $("#emails").val();
        let phone = $("#phone").val();
        let message = $("#message").val();

        if (!name || !email || !phone || !message) {
            alert("Please fill in all required fields.");
            return;
        }

        $.ajax({
            url: 'Contact/Create',
            type: 'POST',
            data: {
                name: name,
                email: email,
                surname: surname
                messageInfo: message
            },
            success: function (response) {

                location.reload();
                $("#name, #email, #surname, #message").val('');
            },
            error: function (error) {
                console.error(error);
            }
        });
    });
