const url = 'https://localhost:44389/';

$(() => {
    if ($("#newItem").length > 0) {
        document.querySelector('#orderId').addEventListener('change', getOrderSummary);
        document.querySelector('#newItem').addEventListener('submit', newItem);
        document.querySelector('#orderCloseButton').addEventListener('click', orderClose);
        fillDropdown("order");
        fillDropdown("product");
    }
    else {
        let orderId = (new URL(document.location)).searchParams.get("orderId");
        $("#orderCode").html(orderId);
        $('#orderId').val(orderId);
        document.querySelector('#orderResetButton').addEventListener('click', orderReset);
        document.querySelector('#orderBackButton').addEventListener('click', () => { location.href = "index.html" });
        getOrderSummary(null);
    }
});


function newItem(event) {

    event.preventDefault();
    const data = {
        orderId: parseInt($("#orderId").val()),
        productId: parseInt($("#productId").val())
    };

    fetch(`${url}order`, {
        method: 'POST',
        headers: { 'Accept': 'application/json', 'Content-Type': 'application/json' },
        body: JSON.stringify(data)
    }).then((response) => {
        getOrderSummary();
        response.json().then(data => {
            if (!data.success) {
                $(".alert-danger").html(data.errorMessage);
                $(".alert-danger").removeClass("elementHidden");
            }
        });

    })

};

function getOrderSummary(event) {

    let orderId = $("#orderId").val();

    $(".alert-danger, .headerLine , #orderCloseButton").addClass("elementHidden");

    fetch(`${url}order/${orderId}`, {
        method: 'GET',
        headers: { 'Accept': 'application/json' },
    }).then(response => {
        response.json().then(function (data) {

            if (event != null) {
                if (data.closed && event.type == 'change') {
                    location.href = `order-closing.html?orderId=${data.id}`;
                    return;
                }
            }

            $("#orderSummaryTitle").html(`Comanda ${data.code}`);
            $("#orderSummary").html("");

            if (data.items.length > 0) {
                
                $.each(data.items, function (key, i) {
                    document.getElementById("orderSummary")
                        .insertAdjacentHTML('afterbegin', `<div class="row"><div class='col-5'>${i.description}</div>
                    <div class='col-2'>${i.amount}</div>
                    <div class='col-5'>${(i.price).toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })}</div></div>`)
                });

                document.getElementById("orderSummary")
                    .insertAdjacentHTML('beforeend', `<div class="row mt-3 discountLine"><div class='col-8'>Total de descontos</div>
                    <div class='col-4 text-right'>- ${(data.discountAmount).toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })}</div></div>`)

                document.getElementById("orderSummary")
                    .insertAdjacentHTML('beforeend', `<div class="row orderAmountLine"><div class='col-8'>Total à pagar</div>
                    <div class='col-4 text-right'>${(data.orderAmount).toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })}</div></div>`)

                $("#orderCloseButton, .headerLine").removeClass("elementHidden");
            }

        });
    })
};

function orderClose() {

    disableButton("orderClose");

    let orderId = $("#orderId").val();

    fetch(`${url}order/${orderId}`, {
        method: 'PUT',
        headers: { 'Accept': 'application/json' }
    }).then(response => {
        if (response.ok) {
            location.href = `order-closing.html?orderId=${orderId}`;
        }
        throw Error(response.statusText);
    })
}

function orderReset() {

    disableButton("orderReset");

    let orderId = $("#orderId").val();

    fetch(`${url}order/${orderId}`, {
        method: 'DELETE',
        headers: { 'Accept': 'application/json' }
    }).then(response => {
        if (response.ok) {
            location.href = `index.html`;
        }
        throw Error(response.statusText);
    })
}

function fillDropdown(field) {

    fetch(`${url}${field}`, {
        method: 'GET',
        headers: { 'Accept': 'application/json' }
    }).then(response => {
        response.json().then(function (data) {
            $.each(data, function (key, p) {
                $(`#${field}Id`).append($('<option></option>').attr('value', p.id).text(p.description));
            });
        });
    })
};


function disableButton(button) {
    $(`#${button}Button`).addClass("disabled").attr("disabled", "disabled");
    $(`#${button}Button>span`).addClass("spinner-border spinner-border-sm mx-2");
}
