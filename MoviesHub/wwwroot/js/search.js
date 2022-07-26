function addPerson(firstname, lastname, tbody, id) {
    const row2 = document.createElement("tr");
    const rowFirstname = document.createElement("td");
    rowFirstname.innerHTML = firstname;
    const rowLastname = document.createElement("td");
    rowLastname.innerHTML = lastname;
    const rowButton = document.createElement("td");
    const rowButtonElement = document.createElement("a");
    rowButtonElement.innerText = "Ajouter";
    rowButton.classList.add("text-center");
    rowButtonElement.classList.add("btn");
    rowButtonElement.classList.add("btn-primary");
    rowButtonElement.setAttribute("href", "Follow/AddUser/" + id);

    rowButton.appendChild(rowButtonElement);
    row2.appendChild(rowFirstname);
    row2.appendChild(rowLastname);
    row2.appendChild(rowButton);
    tbody.appendChild(row2);
}

function addHeader(thead) {
    const row1 = document.createElement("tr");
    const heading = document.createElement("th");
    heading.innerHTML = "Prénom";
    const heading2 = document.createElement("th");
    heading2.innerHTML = "Nom";
    const heading3 = document.createElement("th");
    heading3.innerHTML = "Actions";

    row1.appendChild(heading);
    row1.appendChild(heading2);
    row1.appendChild(heading3);
    thead.appendChild(row1);
}


const searchElement = document.getElementById("search");
const tablePerson = document.getElementById("table-person");

searchElement.addEventListener("keyup",
    function() {
        const input = searchElement.value;

        console.log(input);
        if (input.match(/^ *$/) !== null && document.getElementById("table-person")) {
            document.getElementById("table-person").remove();
        }

        axios.get("/Follow/SearchUser",
                {
                    params: {
                        search: input
                    }
                })
            .then(({ data }) => {
                if (data === null) return;

                const { message } = data;
                const table = document.createElement("table");
                const thead = document.createElement("thead");
                const tbody = document.createElement("tbody");

                if (document.getElementById("table-person")) {
                    document.getElementById("table-person").remove();
                }

                table.appendChild(thead);
                table.appendChild(tbody);
                table.setAttribute("id", "table-person");
                table.classList.add("table");
                table.classList.add("table-striped");
                table.classList.add("table-dark");
                table.classList.add("w-50");
                table.classList.add("m-auto");

                addHeader(thead);

                for (const target of message) {
                    addPerson(target.firstname, target.lastname, tbody, target.id);
                }

                document.getElementById("div-table").appendChild(table);
            })
            .catch(error => {
                console.log("Erreur", error);
            });
    });