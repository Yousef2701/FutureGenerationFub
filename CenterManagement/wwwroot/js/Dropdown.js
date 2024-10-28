window.onload = function() {
    document.getElementById("main-text").style.top = (document.getElementById("home-image").height/-2)+"px";
    };
    
    function toggleDropdown() {
      var dropdown = document.getElementById("myDropdown");
      if (dropdown.style.display === "block") {
        dropdown.style.display = "none";
      } else {
        dropdown.style.display = "block";
      }
    }
    
    document.querySelectorAll("#drop-brands").forEach(e=>{
      e.addEventListener("click",()=>{
        document.getElementById("dropbtn").innerHTML = e.innerHTML;
        document.getElementById("myDropdown").style.display = "none";
      });
    });