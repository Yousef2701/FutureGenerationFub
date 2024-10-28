if (document.body.classList.contains("register")) {
    document.getElementById("registerForm").onsubmit = () => {
        localStorage.setItem("username", document.getElementById("username").value);
    }
}
document.getElementById("completeUsername").value = localStorage.getItem("username");