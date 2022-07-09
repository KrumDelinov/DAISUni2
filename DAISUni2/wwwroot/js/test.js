


//}
//$(".editCourse").mouseover(function () {
//    var button = $(this);
//    var isRunning = button.attr("isRunning");
//    console.log(isRunning)
//    var name = button.data("productname");
//    var alabala = button.attr("alabala");



//});

var items = document.getElementsByClassName("editCourse");

for (var i = 0; i < items.length; i++) {
    items[i].addEventListener("mouseover", deleteProduct);
}
function deleteProduct(e) {
    if (e.target.nodeName == "A" && e.target.innerText == "Edit") {
        
        var bool = e.target.getAttribute("dataId")
        console.log(bool)
      
       
    }

}