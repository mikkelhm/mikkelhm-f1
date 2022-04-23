const api_url = "https://ergast.com/api/f1/seasons.json";

// Defining async function
async function getapi(url) {
    
    var moreResults = true;
    var offset = 0;
    var limit = 30;
    var data = [];
    while(moreResults) {
        var fetchUrl = url+"?limit="+limit+"&offset="+offset;
        console.log(fetchUrl);
        response = await fetch(url+"?limit="+limit+"&offset="+offset);
        var result = await response.json();
        data.push(...result.MRData.SeasonTable.Seasons)
       console.log(data.length);
       console.log(result.MRData.total);
        if(data.length >= result.MRData.total) {
            moreResults = false;
        }
        else {
            offset += limit;
        }
        if(offset > 500)
            moreResults = false;
    }
    showSeasons(data);
    console.log(data);
}
// Calling that async function
getapi(api_url);

// Function to define innerHTML for HTML table
function showSeasons(data) {
    data.sort((a, b) => b.season - a.season);
    // Loop to access all rows 
    let tab = "";
    for (let r of data) {
        tab += `<li>${r.season}: <a href='${r.url}' target='_blank'>${r.url}</a> </li>`;
    }
    // Setting innerHTML as tab variable
    document.getElementById("seasons").innerHTML = tab;
}