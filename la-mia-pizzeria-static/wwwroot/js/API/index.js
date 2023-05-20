loadPizzas(); //ricordati di chiamare la funzione per mostrare le pizze a schermo

function loadPizzas(searchKey) {
    axios.get('/api/Pizzeria', {
        params: {
            search: searchKey
        }
    })
        .then((res) => {        //se la richiesta va a buon fine
            console.log('risposta ok', res);
            if (res.data.length == 0) {     //non ci sono post da mostrare => nascondo la tabella
                document.getElementById('pizzas').classList.add('d-none');
                document.getElementById('no-pizzas').classList.remove('d-none');
            } else {                        //ci sono post da mostrare => visualizzo la tabella
                document.getElementById('pizzas').classList.remove('d-none');
                document.getElementById('no-pizzas').classList.add('d-none');

                //svuoto la tabella
                document.getElementById('pizzas').innerHTML = '';
                res.data.forEach(pizza => {
                    console.log('pizza', pizza);
                    document.getElementById('pizzas').innerHTML +=
                        `
                        <div class="row mb-3">
                            <div class="col d-flex align-items-center fs-4 pb-2">
                                <div class="col-4">
                                    <img src="img/${pizza.image}" style="width: 250px; height: 170px;" />
                                </div>
                                <div class="col-3 text-center fw-bold">
                                    ${pizza.name}
                                </div>
                                <div class="col-2 text-center fw-bold">
                                    ${pizza.price} &euro;
                                </div>
                                <div class="col-3 text-center pl-4">
                                    <a href="/Client/Edit?id=${pizza.Id}" class="btn" style="background-color:green; color:white;">Modifica</a>
                                    <a href="/Client/Details?id=${pizza.Id}" class="btn btn-primary">Dettagli</a><form asp-controller="Pizzeria" asp-action="Delete" asp-route-id="@pizza.Id" method="post">
                                    <a class="btn btn-danger" onclick="deletePizza(${pizza.Id}">Elimina</a>
                                </div>
                            </div>
                        </div>
                        `;
                })
            }
        })
        .catch((res) => {       //se la richiesta non è andata a buon fine
            console.error('errore', res);
            alert('errore nella richiesta');
        });

}