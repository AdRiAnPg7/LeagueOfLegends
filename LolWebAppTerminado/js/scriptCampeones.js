async function mifuncion(i){
    var url = 'http://localhost:56444/api/region/'+i+'/champions';
    //var url = 'http://localhost:3000/Champion';
    const request = await fetch(url);
    try{
        console.log(request);
        const data = await request.json()
        console.log(data);
        document.getElementById("region-"+i+"").innerHTML = 
        `<div class="container">
            <div class="row">
                ${data.map(e =>`
                <div class="col-md-3 col-sm-4 col-xs-12" >
                    <div id="championUni-tile"><img onclick="insertar(${e.id},${i})" src="${e.imgCard}" height="280" width="154"><span>${e.name}</span></div>          
                </div>
                <div id="campeon${e.id}${i}"></div>`
                ).join('')}
            </div>            
        </div>`
    }catch(error){
    }
}

async function insertar(idChamp,idRegion){
    var url = 'http://localhost:56444/api/region/'+idRegion+'/champions/'+idChamp;
    try{
    const request = await fetch(url);
    console.log(request);
    const data = await request.json()
    console.log(data);
    /*document.getElementById("getChampionsBtn").style.display="none";
    document.getElementById("postChampionsBtn").style.display="none";
    document.getElementById("editChampionsBtn").style.display="none";
    document.getElementById("deleteChampionsBtn").style.display="none";
    /*document.getElementById("holasito").style.display="none";*/ 
    
    document.getElementById("campeon"+idChamp+idRegion).innerHTML = 
    `<div class="container">
    <div class="col-md-2 col-sm-2 col-xs-2">

    </div>
    <div class="col-md-10 col-sm-10 col-xs-10 championsito" style="background: url(./img/line/${data.safeLane}.webp) no-repeat center/contain; background-size: cover;">
      <br>      <br>      <br>      <br>      <br>      <br>      <br>      <br>
    </div>
    <div class="col-md-2 col-sm-2 col-xs-2">

    </div>
    <div class="col-md-2 col-sm-2 col-xs-2">

    </div>
    <div class="col-md-10 col-sm-10 col-xs-10" style="background: url(${data.imgBanner}) no-repeat center/contain; background-size: cover;">
      <br>
      <br>
      <img src="./img/Type/${data.type}.png" height="50px" weight="50px">
      <span class="dorado grande" >${data.id}.${data.name}</span>
      <h3 class="izquierda dorado">${data.title}</h3>
      <br><br><br>
      <div class="centro dorado descripcion">${data.description}</div>
      <br><br><br>
      <h2 class="poderes dorado descripcion">${data.skills}</h2>
      
      <br><br>
    </div>
    <div class="col-md-2 col-sm-2 col-xs-2">

    </div>
        
    </div>`
    }catch(error){
    }
}

document.getElementById("newChampionForm").addEventListener("submit", PostChampion)
 async function PostChampion(event){
    event.preventDefault();
    const formElements = await event.target.elements;
    console.log('formelements',formElements.newChampionName.value);
    var url = `http://localhost:56444/api/region/${formElements.idRegionChampionToPost.value}/champions`;
    var data = {
        name: formElements.newChampionName.value,
        title: formElements.newChampionTittle.value,
        safeLane: formElements.newChampionSafeLane.value,
        type: formElements.newChampionType.value,
        description: formElements.newChampionDescription.value,
        skills: formElements.newChampionSkills.value,
        regionId: formElements.idRegionChampionToPost.value,
        imgCard: formElements.newChampionImgCard.value,
        imgIcon: formElements.newChampionImgIcon.value,
        imgBanner: formElements.newChampionImgBanner.value
    };
    await fetch(url, {
    method: 'POST',
    body: JSON.stringify(data),
    headers:{
        'Content-Type': 'application/json'
    }
    }).then(response => response.json())
    .then(data => console.log(data))
    alert("Champ Creado");
    window.location.reload();
}

document.getElementById("putChampionForm").addEventListener("submit",PutChampion)
function PutChampion(event){
    event.preventDefault();
    const formElements = event.target.elements;
    var url = `http://localhost:56444/api/region/${formElements.newChampionIdRegion.value}/champions/${formElements.idToUptade.value}`;
    console.log('formelements',formElements.newChampionIdRegion.value);
    console.log('formelements',formElements.idToUptade.value);
    var data = {
        id:  formElements.idToUptade.value,
        name: formElements.newChampionName.value,
        title: formElements.newChampionTittle.value,
        safeLane: formElements.newChampionSafeLane.value,
        type: formElements.newChampionType.value,
        description: formElements.newChampionDescription.value,
        skills: formElements.newChampionSkills.value,
        regionId: formElements.newChampionIdRegion.value,
        imgCard: formElements.newChampionImgCard.value,
        imgIcon: formElements.newChampionImgIcon.value,
        imgBanner: formElements.newChampionImgBanner.value
    };
    fetch(url, {
        headers: { "Content-Type": "application/json; charset=utf-8" },
        method: 'PUT',
        body: JSON.stringify(data)
    })
    alert("Champ Editado");
    window.location.reload();   
}

document.getElementById("deleteChampionForm").addEventListener("submit",DeleteChampion)
async function DeleteChampion(event){
    event.preventDefault();
    const formElements = await event.target.elements;
    console.log('formelements',formElements.idChampionToDelete.value);
    //var url = `http://localhost:3000/Champion/${formElements.idChampionToDelete.value}`;
    var url = `http://localhost:56444/api/region/${formElements.idRegionChampionToDelete.value}/champions/${formElements.idChampionToDelete.value}`;
    await fetch(url, { 
        method: 'DELETE' 
    })
    .then(response => response.json())
    .then(data => console.log(data))
    alert("Champ Eliminado");
    window.location.reload();   
}
document.getElementById("getChampionsBtn").addEventListener("click", 
async function(){
    try{
        for (var i = 1; i < 13; i++) {
            mifuncion(i);
         }
        document.getElementById("championsContainer").innerHTML = 
        `
        <div id="region-1"></div>
        <div id="region-2"></div>
        <div id="region-3"></div>
        <div id="region-4"></div>
        <div id="region-5"></div>
        <div id="region-6"></div>
        <div id="region-7"></div>
        <div id="region-8"></div>
        <div id="region-9"></div>
        <div id="region-10"></div>
        <div id="region-11"></div>
        <div id="region-12"></div>
        <div id="region-13"></div>

        `
    }catch(error){
    }
});