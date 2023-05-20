
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

        "image": document.getElementById("pizzaImage").value,
        "name": document.getElementById("pizzaName").value,
        "description": document.getElementById("pizzaDescription").value,
        "price": document.getElementById("pizzaPrice").value,
        "pizzaCategoryId": document.getElementById('pizzaCategory').value
    };
    axios.post("/api/Pizzeria", pizzaToCreate)
        .then((res) => {
            alert("pizza creata correttamente!");
            window.location.href = "/Client";
        })
        .catch((res) => {       //se la richiesta non è andata a buon fine
            console.error('errore', res);
            alert('errore nella richiesta');
        });
}

function loadPizzaCategories() {
    axios.get('/api/PizzaCategories')
        .then((res) => {        //se la richiesta va a buon fine
            console.log('risposta ok', res);
            document.getElementById('pizzaCategory').innerHTML = '<option value=""></option>';

            res.data.forEach(pizzaCategory => {
                document.getElementById('pizzaCategory').innerHTML +=
                    `<option value="${pizzaCategory.id}">${pizzaCategory.name}</option>`;
            })
        })
        .catch((res) => {       //se la richiesta non è andata a buon fine
            console.error('errore', res);
            alert('errore nella richiesta');
        });
}