 let array = document.getElementById("data").textContent;
            let arrayOfStudentAnswers = [];
            let numberOfQuestions = 3;
            let questionNumber = 1;
            let check = 0;
            let notchecked = 0;
            //the time of the exam
            let minutes = document.getElementById("min").textContent;
            let seconds = 0;
            document.getElementById("data").remove();
            document.getElementById("min").remove();
            let timer = setInterval(() => {
              seconds--;
              if (seconds === -1) {
                minutes--;
                seconds = 59;
              }
              document.getElementById("minutes").textContent = minutes;
              document.getElementById("seconds").textContent = seconds;
              if (minutes === 0 && seconds === 0) {
                document.getElementById("next").style.backgroundColor = "#1c7307";
                document.getElementById("next").setAttribute("type", "submit");
                document.getElementById("next").textContent = "Finish";
                document
                  .getElementById("minutes")
                  .parentElement.classList.add("d-none");
                document.getElementById("next").parentElement.submit();
              }
            }, 1000);
            window.onload = () => {
              document.getElementById("spans").children[0].classList.add("on");
            };
            for (let i = 0; i < numberOfQuestions; i++) {
              const span = document.createElement("span");
              document.getElementById("spans").appendChild(span);
            }
            document.querySelectorAll(".answer").forEach((ele) => {
              ele.onclick = () => {
                ele.children[0].click();
              };
            });
            document.querySelectorAll(".answer input").forEach((ele) => {
              ele.onclick = () => {
                  document.querySelectorAll(".answer input").forEach((element) => {
                  element.parentElement.style.backgroundColor = "#f9f9f9";
                });
                ele.parentElement.style.backgroundColor = "#2199e8";
                localStorage.setItem(questionNumber, ele.getAttribute("id"));
              };
            });
            document.getElementById("next").onclick = () => {
                document.querySelectorAll(".answer input").forEach((element) => {
                if (element.checked === true) {
                    arrayOfStudentAnswers[check] = element.nextElementSibling.textContent;
                    document.getElementById("returnData").textContent = arrayOfStudentAnswers;
                    console.log(document.getElementById("returnData").textContent);
                  check++;
                  if (check === questionNumber) {
                    if (questionNumber === numberOfQuestions - 1) {
                      document.getElementById("next").style.backgroundColor =
                        "#1c7307";
                      document.getElementById("next").textContent = "Finish";
                    }
                    if (questionNumber === numberOfQuestions) {
                      document.getElementById("next").setAttribute("type", "submit");
                    }
                    questionNumber++;
                    document.getElementById("questionNumber").textContent =
                      questionNumber;
                    for (let i = 0; i < questionNumber; i++) {
                      document
                        .getElementById("spans")
                        .children[i].classList.add("on");
                    }
                    document.querySelectorAll("input").forEach((element) => {
                      element.parentElement.style.backgroundColor = "#f9f9f9";
                      element.checked = false;
                    });
                  }
                } else {
                  notchecked++;
                  if (notchecked === 4) {
                    //alert("Please select an answer.");
                  }
                }
              });
              notchecked = 0;
            };
            document.getElementById("next").parentElement.onsubmit = () => {
              for (let i = 0; i < questionNumber; i++) {
                localStorage.removeItem(i);
              }
            };
            document.getElementById("GPA").onsubmit = ()=>{
                console.log(arrayOfStudentAnswers);
                document.getElementById("returnData").textContent = arrayOfStudentAnswers;

            }