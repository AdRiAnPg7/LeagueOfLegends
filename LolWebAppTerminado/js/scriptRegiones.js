document.getElementById("getRegionsBtn").addEventListener("click", 
async function(){
    try{
        const request = await fetch('http://localhost:56444/api/region');
        console.log(request);
        const data = await request.json()
        console.log(data);
        document.getElementById("regionsContainer").innerHTML = 
        `<div id="carouselExampleIndicators" class="carousel slide" data-ride="carousel">
            <ol class="carousel-indicators">
                <li data-target="#carouselExampleIndicators" data-slide-to="0" class="active"></li>
                <li data-target="#carouselExampleIndicators" data-slide-to="1"></li>
                <li data-target="#carouselExampleIndicators" data-slide-to="2"></li>
                <li data-target="#carouselExampleIndicators" data-slide-to="3"></li>
                <li data-target="#carouselExampleIndicators" data-slide-to="4"></li>
                <li data-target="#carouselExampleIndicators" data-slide-to="5"></li>
                <li data-target="#carouselExampleIndicators" data-slide-to="6"></li>
                <li data-target="#carouselExampleIndicators" data-slide-to="7"></li>
                <li data-target="#carouselExampleIndicators" data-slide-to="8"></li>
                <li data-target="#carouselExampleIndicators" data-slide-to="9"></li>
                <li data-target="#carouselExampleIndicators" data-slide-to="10"></li>
                <li data-target="#carouselExampleIndicators" data-slide-to="11"></li>
                <li data-target="#carouselExampleIndicators" data-slide-to="12"></li>
                <li data-target="#carouselExampleIndicators" data-slide-to="13"></li>
            </ol>

            <div class="carousel-inner">

                <div class="carousel-item active">
                    <img class="d-block w-100" height="640px" weight="360px" src="img/active.jpg" alt="Active slide">
                </div>
                ${data.map(e =>`
                <div class="carousel-item">
                    <img class="d-block w-100" height="640px" weight="360px" src="${e.imgCrsl}">
                    <div class="carousel-caption d-none d-md-block">
                        <h5>${e.id}. ${e.name}</h5>
                        <p>${e.description}</p>
                    </div>
                </div>`
                
                ).join('')}
                
            </div>
            <a class="carousel-control-prev" href="#carouselExampleIndicators" role="button" data-slide="prev">
              <span class="carousel-control-prev-icon" aria-hidden="true"></span>
              <span class="sr-only">Previous</span>
            </a>
            <a class="carousel-control-next" href="#carouselExampleIndicators" role="button" data-slide="next">
              <span class="carousel-control-next-icon" aria-hidden="true"></span>
              <span class="sr-only">Next</span>
            </a>
        </div>`
        document.getElementById("regionsContainer2").innerHTML = 
        `
        <div class="container" style="background: rgba(0, 0, 0, 0.5);">
            ${data.map(f =>`
            <span><img src="${f.imgLogo}" widht="100px" height="160px"></span><span class="dorado titulosito" >${f.id}.${f.name}</span>
            <h3 class="dorado">${f.description}</h3>
            <div><button onclick="insertarDataRegion(${f.id})" class="btn btn-success" >Ver Campeones de esta Region</button></div>
            <div id="region${f.id}"></div>`
            ).join('')}
        </div>`
    }catch(error){
    }
});

async function insertarDataRegion(i){
    var url = 'http://localhost:56444/api/region/'+i+'/champions';
    try{
    const request = await fetch(url);
    console.log(request);
    const data = await request.json()
    console.log(data);
    
    document.getElementById("region"+i+"").innerHTML = 
    `
    <div class="container" style="background: rgba(250, 250, 250, 0.8);">
        <div class="row">
            ${data.map(g =>`

            <div class="col-md-2 col-sm-3 col-xs-4 iconsito">
                <img class="imagensita" onclick="insertarDataCampeonRegion(${g.id},${i})" src="${g.imgIcon}" height="60px" width="60px">
            </div>
            <div id="regionCampeon${g.id}${i}"></div>
            `

            ).join('')}
        </div>
    </div>
    `
    }catch(error){
    }
}

async function insertarDataCampeonRegion(iChamp,iRegion){
    var url = 'http://localhost:56444/api/region/'+iRegion+'/champions/'+iChamp;
    //var url = 'http://localhost:3000/Champion/'+iChamp;
    console.log(iChamp);console.log(iRegion);
    try{
    const request = await fetch(url);
    console.log(request);
    const data = await request.json()
    console.log(data);
    
    document.getElementById("regionCampeon"+iChamp+iRegion+"").innerHTML = 
    `
    <div class="container">
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
        
    </div>
    `
    }catch(error){
    }
}



document.getElementById("newRegionForm").addEventListener("submit", PostRegion)
 async function PostRegion(event){
    event.preventDefault();
    const formElements = await event.target.elements;
    console.log('formelements',formElements.newRegionName.value);
    var url = 'http://localhost:56444/api/region';
    var data = {
        name: formElements.newRegionName.value,
        description: formElements.newRegionDescription.value,
        imgLogo: formElements.newRegionImgLogo.value,
        imgCrsl: formElements.newRegionImgCrsl.value,
        imgBanner: formElements.newRegionImgBanner.value
        
    };
    await fetch(url, {
    method: 'POST',
    body: JSON.stringify(data),
    headers:{
        'Content-Type': 'application/json'
    }
    }).then(response => response.json())
    .then(data => console.log(data))
    alert("Region Creada");
    window.location.reload();
}

document.getElementById("putRegionForm").addEventListener("submit",PutRegion)
function PutRegion(event){
    event.preventDefault();
    const formElements = event.target.elements;
    var url = `http://localhost:56444/api/region/${formElements.idToUptade.value}`;
    var data = {
        id:  formElements.idToUptade.value,
        name: formElements.newRegionName.value,
        description: formElements.newRegionDescription.value,
        imgLogo: formElements.newRegionImgLogo.value,
        imgCrsl: formElements.newRegionImgCrsl.value,
        imgBanner: formElements.newRegionImgBanner.value
    };
    fetch(url, {
        headers: { "Content-Type": "application/json; charset=utf-8" },
        method: 'PUT',
        body: JSON.stringify(data)
    })
    alert("Region Editada");
    window.location.reload();   
}

document.getElementById("deleteRegionForm").addEventListener("submit",DeleteRegion)
async function DeleteRegion(event){
    event.preventDefault();
    const formElements = await event.target.elements;
    console.log('formelements',formElements.idRegionToDelete.value);
    var url = `http://localhost:56444/api/region/${formElements.idRegionToDelete.value}`;
    await fetch(url, { 
        method: 'DELETE' 
    })
    .then(response => response.json())
    .then(data => console.log(data))
    alert("region Eliminado");
    window.location.reload();   
}