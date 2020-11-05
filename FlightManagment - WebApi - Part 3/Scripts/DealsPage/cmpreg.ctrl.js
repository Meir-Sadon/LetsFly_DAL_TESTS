module.controller('cmpRegCtrl', CmpRegCtrl)

function CmpRegCtrl($scope, $http, apiService, dataService) {

    $scope.form = {
        airlineName: "",
        email: "",
        password: "",
        country: "",
        agree: false
    }

    apiService.getToken(dataService.tokenUserName, dataService.tokenPassword).then(() => {
        $scope.token = dataService.token
    })

    apiService.ListenAndReadFromCompaniesRequestsQueue()

    $scope.tryWhenInvalid = false;
    $scope.allCountries = ["Choose Country"]

    // Get All Countries List From Data Base.
    apiService.getAllItems(dataService.matchingVacancyFlightsUrl).then(() => {
        $.each(dataService.allCountries, function (i, country) {
            $scope.allCountries.push(country.Country_Name)
        });
    })

    // The Actions Will Be Charged When The Registration Button Will Be Click.
    $scope.onSubmit = function (formIsValid) {
        if (formIsValid) {
            $scope.tryWhenInvalid = false;
            const company = {
                Airline_Name: $scope.form.airlineName,
                User_Name: $scope.form.email,
                Password: $scope.form.password,
                Country_Code: $scope.form.country,
            };
            $http.post("api/toqueue/company", JSON.stringify(company))
                .then(
                    (resp) => {
                        console.log(resp);
                        Swal.fire({
                            position: 'center',
                            icon: 'success',
                            title: `${resp.data}`,
                            showConfirmButton: true,
                        });
                    },
                    // error
                    (err) => {
                        console.log(err);
                        Swal.fire({
                            position: 'center',
                            icon: 'error',
                            title: `${err.data.message}`,
                            showConfirmButton: true,
                        });
                    }
                )
        } else {
            $scope.tryWhenInvalid = true
        }
    }

    // Watch For Airline Name Input.
    $scope.$watch('form.airlineName', (newAirlineName) => {
        elementIsValid(newAirlineName, /[a-zA-Z][^#&<>\"~;$^%{}?]{1,20}$/g, $scope.regForm.airlineName, $("#airlinename-validate"))
    })

    // Watch For Email Input.
    $scope.$watch('form.email', (newEmail) => {
        elementIsValid(newEmail, /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/, $scope.regForm.email, $("#email-validate"))
    })

    // Watch For Password Input.
    $scope.$watch('form.password', (newPassword) => {
        elementIsValid(newPassword, /^(?=.*?[0-9])(?=.*?[A-Z])(?=.*?[#?!@$%^&*\-_]).{8,}$/, $scope.regForm.pass, $("#pass-validate"))
    })

    // Watch For Country Input.
    $scope.$watch('form.country', (newCountry) => {
        if (!$scope.regForm.country.$pristine) {
            if (newCountry != "Choose Country") {
                $scope.regForm.country.$valid = true
                $("#country-validate").removeClass('alert-validate')
            } else {
                $scope.regForm.country.$valid = false
                $("#country-validate").addClass('alert-validate')
            }
        }
    })

    // Watch For TermsAgree Input.
    $scope.$watch('form.agree', (newVal) => {
        if (!$scope.regForm.termsAgree.$pristine) {
            if (newVal == true) {
                $scope.regForm.termsAgree.$valid = true
                $("#agree-validate").removeClass('alert-validate-agree')
            } else {
                $scope.regForm.termsAgree.$valid = false
                $("#agree-validate").addClass('alert-validate-agree')
            }
        }
    })

    // Generic Function That Check If The Input Match To Reg
    function elementIsValid(elm, reg, nameFromHtml, elmFromHtml) {
        if (!nameFromHtml.$pristine) {
            if (reg.test(String(elm))) {
                elmFromHtml.removeClass('alert-validate')
                nameFromHtml.$valid = true
            } else {
                elmFromHtml.addClass('alert-validate')
                nameFromHtml.$valid = false
            }
        }
    }

    // Add/Remove Error-Alert When Try To Sign-Up
    $scope.$watch('tryWhenInvalid', (newVal) => {
        if (newVal) {
            if (!$scope.regForm.airlineName.$valid)
                $("#airlinename-validate").addClass('alert-validate')
            if (!$scope.regForm.email.$valid)
                $("#email-validate").addClass('alert-validate')
            if (!$scope.regForm.pass.$valid)
                $("#pass-validate").addClass('alert-validate')
            if (!$scope.regForm.country.$valid)
                $("#country-validate").addClass('alert-validate')
            if (!$scope.regForm.termsAgree.checked)
                $("#agree-validate").addClass('alert-validate-agree')
        } else {
            $("#airlinename-validate").removeClass('alert-validate')
            $("#email-validate").removeClass('alert-validate')
            $("#pass-validate").removeClass('alert-validate')
            $("#country-validate").removeClass('alert-validate')
            $("#agree-validate").removeClass('alert-validate-agree')
        }
    })

}