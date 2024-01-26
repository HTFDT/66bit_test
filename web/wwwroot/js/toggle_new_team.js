const checkbox = document.querySelector("#ViewModel_IsNewTeam");
const existingTeamInput = document.querySelector(".team-input--existing");
const newTeamInput = document.querySelector(".team-input--new");

document.addEventListener('DOMContentLoaded', function (){
    if (checkbox.checked){
        newTeamInput.classList.add("active")
    }
    else{
        existingTeamInput.classList.add("active");
    }
})

checkbox.addEventListener("click", function (){
    if (this.checked){
        !newTeamInput.classList.contains("active") && newTeamInput.classList.add("active");
        existingTeamInput.classList.remove("active");
    } else{
        newTeamInput.classList.remove("active");
        !existingTeamInput.classList.contains("active") && existingTeamInput.classList.add("active");
    }
})