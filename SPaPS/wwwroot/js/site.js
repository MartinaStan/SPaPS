$(document).ready(function () {
	$('.dropdown-select').select2({
		allowClear: true,
		theme: 'bootstrap-5'
	});


	let roleInput = document.querySelector("#Role");
	let noOfEmpInput = document.querySelector(".NoOfEmployees");
	let dateOfEstInput = document.querySelector(".DateOfEstablishment");
	let activitiesInput = document.querySelector(".Activities");

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
			document.querySelector("#Activities").value = null;
		}
	})
});

var form = document.querySelector("#register");
var noOfEmpInput = document.querySelector(".NoOfEmployees");
var dateOfEstInput = document.querySelector(".DateOfEstablishment");
var activitiesInput = document.querySelector(".Activities");


document.addEventListener('DOMContentLoaded', () => {
	form.addEventListener('submit', function (e) {

		if (noOfEmpInput.value == null || dateOfEstInput.value == null || activitiesInput.value == null) {
			/*e.preventDefault()*/
			alert("Полето е задолжително")
			
			return false;
		}
		else {
			return true;
		}
	})
});


	

//$(function () {
//	var date = new Date();
//	var currentMonth = date.getMonth();
//	var currentDate = date.getDate();
//	var currentYear = date.getFullYear();
//	$('.DateOfEstablishment').datepicker({
//		maxDate: new Date(currentYear, currentMonth, currentDate)
//	});
//});