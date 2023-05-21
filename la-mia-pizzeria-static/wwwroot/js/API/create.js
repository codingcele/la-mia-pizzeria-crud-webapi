
loadPizzaCategories();

function creaPizza() {
    pizzaToCreate = {
        //"image": "prosciutto-e-funghi.jpg",
        //"name": "Pizzaahhh",
        //"description": "la coppia vffds.",
        //"price": 9.50,
        //"pizzaCategoryId": 3,
        //"pizzaCategory": null,
        //"ingredients": null

        "image": document.getElementById("image").value,
        "name": document.getElementById("name").value,
        "description": document.getElementById("description").value,
        "price": document.getElementById("price").value == 0 ? null : document.getElementById('category').value,
        "pizzaCategoryId": document.getElementById('category').value == '' ? null : document.getElementById('category').value,
    };
    axios.post("/api/Pizzeria", pizzaToCreate)
        .then((res) => {
            alert("pizza creata correttamente!");
            window.location.href = "/Client";
        })
        .catch((res) => {       //se la richiesta non è andata a buon fine
            for (let errorKey in res.response.data.errors) {
                // testo errore: res.response.data.errors[errorKey]
                let spanId = errorKey.toLowerCase() + "Validation";
                let span = document.getElementById(spanId);
                span.innerHTML = res.response.data.errors[errorKey];
                console.log('Errore: ' + res.response.data.errors[errorKey]);
            }
            console.error('errore', res);
        });
}

function loadPizzaCategories() {
    axios.get('/api/PizzaCategories')
        .then((res) => {        //se la richiesta va a buon fine
            console.log('risposta ok', res);
            document.getElementById('category').innerHTML = '<option value=""></option>';

            res.data.forEach(category => {
                document.getElementById('category').innerHTML +=
                    `<option value="${category.id}">${category.name}</option>`;
            })
        })
        .catch((res) => {       //se la richiesta non è andata a buon fine
            console.error('errore', res);
            alert('errore loadcategories');
        });
}