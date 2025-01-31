let answers = [];
let minutes = document.getElementById("getTime").textContent;
let seconds = 0;
let timer = setInterval(() => {
    seconds--;
    if (seconds === -1) {
        minutes--;
        seconds = 59;
    }
    document.getElementById("minutes").textContent = minutes;
    document.getElementById("seconds").textContent = seconds;
    if (minutes === 0 && seconds === 1 || minutes < 0) {
        document.getElementById("submit").click();
    }
}, 1000);
document.querySelectorAll("input[type=radio]").forEach(e => {
    e.onchange = () => {
        e.parentElement.querySelectorAll("label").forEach(ele => {
            ele.style.backgroundColor = "white";
        })
        if (e.checked == true) {
            e.nextElementSibling.style.backgroundColor = "#86b7fe";
        }
    }
})
document.getElementById("submitForm").onsubmit = () => {
    document.querySelectorAll("input[type=radio]").forEach(e => {
        if (e.checked == true) {
            answers.push(e.id.substring(e.id.length - 1, e.id.length));
        }
    })
    document.getElementById("storedData").value = answers;
}


