$(document).ready(function () {
	$('.dropdown-select').select2({
		allowClear: true,
		theme: 'bootstrap-5'
	});


	let roleInput = document.querySelector("#Role");
	let noOfEmpInput = document.querySelector(".NoOfEmployees");
	let dateOfEstInput = document.querySelector(".DateOfEstablishment");
	let activitiesInput = document.querySelector(".Activities");

	if (roleInput) {
		roleInput.addEventListener("change", function () {
			if (roleInput.value == "Изведувач") {
				noOfEmpInput.classList.remove("d-none");
				dateOfEstInput.classList.remove("d-none");
				activitiesInput.classList.remove("d-none");
			}
			else {
				noOfEmpInput.classList.add("d-none")
				dateOfEstInput.classList.add("d-none")
				activitiesInput.classList.add("d-none")

				document.querySelector("#NoOfEmployees").value = null;
				document.querySelector("#DateOfEstablishment").value = null;
				document.querySelector("#Activities").value = '';
			}
		})
	}


	let serviceInput = document.querySelector("#ServiceId");

	if (serviceInput) {
		let buildingType = document.querySelector(".BuildingTypeId");
		let buildingSize = document.querySelector(".BuildingSize");
		let color = document.querySelector(".Color");
		let noOfWindows = document.querySelector(".NoOfWindows");
		let noOfDoors = document.querySelector(".NoOfDoors");
		let NoOfLights = document.querySelector(".NoOfLights");
		let NoOfSockets = document.querySelector(".NoOfSockets");
		let NoOfCarpets = document.querySelector(".NoOfCarpets");

		serviceInput.addEventListener("change", function () {

			buildingType.classList.add("d-none");
			buildingSize.classList.add("d-none");
			color.classList.add("d-none");
			noOfWindows.classList.add("d-none");
			noOfDoors.classList.add("d-none");
			NoOfLights.classList.add("d-none");
			NoOfSockets.classList.add("d-none");
			NoOfCarpets.classList.add("d-none");

			let serviceId = serviceInput.value;

			if (serviceId == null) {
				return;
			}

			if (serviceId == 1) {
				buildingType.classList.remove("d-none");
				noOfWindows.classList.remove("d-none");
				noOfDoors.classList.remove("d-none");
			}

			if (serviceId == 3) {
				buildingType.classList.remove("d-none");
				buildingSize.classList.remove("d-none");
				color.classList.remove("d-none");
			}

			if (serviceId == 4) {
				buildingType.classList.remove("d-none");
				buildingSize.classList.remove("d-none");
			}
			if (serviceId == 6) {
				buildingType.classList.remove("d-none");
				NoOfLights.classList.remove("d-none");
				NoOfSockets.classList.remove("d-none");
			}
			if (serviceId == 7) {
				NoOfCarpets.classList.remove("d-none");
			}
		});

	}




	var form = document.querySelector("#EditRequest");
	form.addEventListener("click", function () {

		let buildingType = document.querySelector(".BuildingTypeId");
		let buildingSize = document.querySelector(".BuildingSize");
		let color = document.querySelector(".Color");
		let noOfWindows = document.querySelector(".NoOfWindows");
		let noOfDoors = document.querySelector(".NoOfDoors");
		let NoOfLights = document.querySelector(".NoOfLights");
		let NoOfSockets = document.querySelector(".NoOfSockets");
		let NoOfCarpets = document.querySelector(".NoOfCarpets");

		serviceInput.addEventListener("change", function () {

			buildingType.classList.add("d-none");
			buildingSize.classList.add("d-none");
			color.classList.add("d-none");
			noOfWindows.classList.add("d-none");
			noOfDoors.classList.add("d-none");
			NoOfLights.classList.add("d-none");
			NoOfSockets.classList.add("d-none");
			NoOfCarpets.classList.add("d-none");

			let serviceId = serviceInput.value;

			if (serviceId == null) {
				return;
			}

			if (serviceId == 1) {
				buildingType.classList.remove("d-none");
				noOfWindows.classList.remove("d-none");
				noOfDoors.classList.remove("d-none");
			}

			if (serviceId == 3) {
				buildingType.classList.remove("d-none");
				buildingSize.classList.remove("d-none");
				color.classList.remove("d-none");
			}

			if (serviceId == 4) {
				buildingType.classList.remove("d-none");
				buildingSize.classList.remove("d-none");
			}
			if (serviceId == 6) {
				buildingType.classList.remove("d-none");
				NoOfLights.classList.remove("d-none");
				NoOfSockets.classList.remove("d-none");
			}
			if (serviceId == 7) {
				NoOfCarpets.classList.remove("d-none");
			}
		})

    })
	
	
});




var form = document.querySelector("#register");
var noOfEmpInput = document.querySelector("#NoOfEmployees");
var dateOfEstInput = document.querySelector("#DateOfEstablishment");
var activitiesInput = document.querySelector("#Activities");

let roleInput = document.querySelector("#Role");


/*document.addEventListener('DOMContentLoaded', () => {*/
	if (roleInput) {
		if (roleInput == "Изведувач") {
			form.addEventListener('submit', function (e) {

				if (IsNullOrEmpty(noOfEmpInput.value) || IsNullOrEmpty(dateOfEstInput.value) || IsNullOrEmpty(activitiesInput.value)) {
					e.preventDefault()
					alert("Полето е задолжително")

					return false;
				}
				else {
					/*e.currentTarget.submit();*/
					return true;
				}
			})
		}
    }
	



function IsNullOrEmpty(input) {

	if (input == null || input == "" ||  input == undefined || input == " ") {
		return true;
	}

	return false;
}

	

//$(function () {
//	var date = new Date();
//	var currentMonth = date.getMonth();
//	var currentDate = date.getDate();
//	var currentYear = date.getFullYear();
//	$('.DateOfEstablishment').datepicker({
//		maxDate: new Date(currentYear, currentMonth, currentDate)
//	});
//});