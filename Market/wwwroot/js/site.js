const BASE_URL = "https://localhost:5001"

async function openModal(URL, id) {    
    const modal = $("#modal")  
    
    axios.get(BASE_URL + URL, {params: {id: id}}).then(response => {    
    modal.find(".modal-body").html(response.data)
    modal.modal("show")            
    }).catch(ex => {
        alert(ex)
        modal.modal("hide")
    })             
}


//function openModal(params) {   
//    const id = params.data;
//    const url = params.url;

//    const modal = $("#modal");

//    if (id == undefined || url == undefined) {
//        alert("Error -_-")
//    }

//    $.ajax({
//        type: "Get",
//        url : url,
//        data: { "id": id },        
//        success: function(response) {
//            modal.find(".modal-body").html(response);
//            modal.modal("show")         
//            debugger;
//        },
//        failure: function () {
//            modal.modal("hide")
//        },
//        error: function (response) {
//            alert(response.responseText)
//        }
//    })
//}