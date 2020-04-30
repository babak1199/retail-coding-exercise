// TODO: It would be much better to modularize JS code

// --- Search form handling module
$(function () {
    var $form = $('#productSearch');

    if (!$form.length) {
        console.error('Search form not found.');
    }

    if (!$form.validate) {
        console.error('jQuery validation not loaded.');
    }

    $form.validate({
        errorLabelContainer: "#messageBox",
        rules: {
            query: {
                required: true,
                minlength: 2
            }
        },
        messages: {
            query: {
                required: "Search string cannot be empty",
                minlength: "Keep typing until you entered at least 2 characters"
            }
        },
        errorPlacement: function (error, element) {
            error.appendTo('div.error');
        }
    });

    $form.on('submit', function (e) {
        if (!$form.valid()) {
            console.info('Search form is invalid and won\'t be submitted.');
            e.preventDefault();
            return false;
        }
    });
});

// --- Cart handling module
$(function () {
    $('a[data-product-id]').on('click', function () {
        var $this = $(this);
        var data = {
            productId: $this.data('product-id'),
            cartId: '07B03D2C-8AAE-11EA-84A2-6C46FC2CA371'
        };
        $.ajax({
            url: 'api/cart',
            type: "POST",
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                console.info(data);
            }
        });
    });
});