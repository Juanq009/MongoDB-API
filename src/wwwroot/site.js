var contenido = document.querySelector("#contenido")
function traer()
{
    fetch("https://localhost:5001/api/values")
    .then(res => res.json())
    .then(data => {
        tabla(data)
    })
}

function tabla(data){
    contenido.innerHTML= ``
    for (let ind of data){
        contenido.innerHTML += `
        
        <tr>
        <th scope="row">${ind._id}</th>
        <td>${ind.nombre}</td>
        <td>${ind.apellido}</td>
        <td>${ind.edad}</td>
      </tr>
      `
        
    }
}