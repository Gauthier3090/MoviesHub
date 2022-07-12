const searchElement = document.getElementById("search");
const resultElement = document.getElementById('result');

searchElement.addEventListener("keyup",
    function() {
        const input = searchElement.value;

        axios.get("/Follow/SearchUser",
                {
                    params: {
                        search: input
                    }
                })
            .then(({ data }) => {
                if (data === null) return;

                const { message } = data;

                while (resultElement.firstChild) {
                    resultElement.removeChild(resultElement.firstChild);
                }


                if (message.length > 0) {

                    for (const target of message) {

                        console.log(target);

                        const element = document.createElement('p');
                        element.innerText = target.firstname;

                        resultElement.appendChild(element);
                    }
                    
                }

            })
            .catch(error => {
                console.log("Erreur", error);
            });
    });