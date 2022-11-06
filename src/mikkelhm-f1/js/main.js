const api_url = "https://azfunc-mikkelhm-f1-api.azurewebsites.net/api/seasons";

// Defining async function
async function getapi(url) {

    var data = [];
    console.log(url);
    response = await fetch(url);
    var result = await response.json();
    data.push(...result)
    console.log(result);
    showSeasons(data);
    console.log(data);
}
// Calling that async function
getapi(api_url);

// Function to define innerHTML for HTML table
function showSeasons(data) {
    data.sort((a, b) => b.year - a.year);
    // Loop to access all rows 
    let tab = "";
    for (let r of data) {
        tab += `<li>${r.year}: <a href='${r.wikipediaInformation.link}' target='_blank'>${r.wikipediaInformation.link}</a> </li>`;
    }
    // Setting innerHTML as tab variable
    document.getElementById("seasons").innerHTML = tab;
}