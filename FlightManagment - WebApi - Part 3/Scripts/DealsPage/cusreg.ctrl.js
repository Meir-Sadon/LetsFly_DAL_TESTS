module.controller('cusRegCtrl', CusRegCtrl)

function CusRegCtrl($scope, $http, globalConst, dataService) {

    $scope.form = {
        fullName: '',
        email: '',
        password: '',
        address: { country: '', state: '', city: '' },
        phoneNumber: "",
        card: "",
        agree: false
    }

    $scope.allCountries = globalConst.all_countries_list
    $scope.countryStates = ["Choose State"]
    $scope.stateCities = ["Choose City"]
    $scope.tryWhenInvalid = false;

    // The Actions Will Be Charged When The Registration Button Will Be Click.
    $scope.onSubmit = function(formIsValid) {
        if (formIsValid) {
            $scope.tryWhenInvalid = false;
            const customer = {
                First_Name: $scope.form.fullName.split(' ')[0],
                Last_Name: $scope.form.fullName.split(' ')[1],
                User_Name: $scope.form.email,
                Password: $scope.form.password,
                Address: $scope.form.address.country + ":" + $scope.form.address.state + ":" + $scope.form.address.city,
                Phone_No: $scope.form.phoneNumber,
                Credit_Card_Number: $scope.form.card
                };
                $http.post("api/create/customer", JSON.stringify(customer))
                .then(
                    (resp) => {
                        Swal.fire({
                            position: 'center',
                            icon: 'success',
                            title: `${resp.data}`,
                            showConfirmButton: true,
                        });
                    },
                    // error
                    (err) => {
                        Swal.fire({
                            position: 'center',
                            icon: 'error',
                            title: `${err.data}`,
                            showConfirmButton: true,
                        });
                    }
                )
        }
        if (!formIsValid) {
            $scope.tryWhenInvalid = true;
            }
    }


    // Watch For Full Name Input.
    $scope.$watch('form.fullName', (newFullName) => {
        elementIsValid(newFullName, /^[a-zA-Z]+ [a-zA-Z]+$/, $scope.regForm.name, $("#fullname-validate"))
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
    $scope.$watch('form.address.country', (newCountry) => {
        if (!$scope.regForm.country.$pristine) {
            if (checkIfSelectInputIsValid("Country")) {
                $scope.updateStatesOptions(newCountry)
                $scope.regForm.country.$valid = true
            } else {
                $scope.regForm.country.$valid = false
            }
            checkCtrStateAndCity()
        }
    })
    
    // Watch For State Input.
    $scope.$watch('form.address.state', (newState, oldState) => {
        if (!$scope.regForm.state.$pristine) {
            if (checkIfSelectInputIsValid("State")) {
                $scope.updateCitiesOptions($scope.form.address.country, newState)
                $scope.regForm.state.$valid = true
            } else {
                $scope.regForm.state.$valid = false
            }
            checkCtrStateAndCity()
        }
    })

    // Watch For City Input.
    $scope.$watch('form.address.city', (newCity) => {
        if (!$scope.regForm.city.$pristine) {
            if (checkIfSelectInputIsValid("City")) {
                $scope.regForm.city.$valid = true
            } else {
                $scope.regForm.city.$valid = false
            }
            checkCtrStateAndCity()
        }
    })

    // Watch For Phone Number Input.
    $scope.$watch('form.phoneNumber', (newNumber) => {
        elementIsValid(newNumber, /^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{3,6}$/im, $scope.regForm.phNumber, $("#ph-number-validate"))
    })

    // Function That Checked If Country And City Input Is Valid.
    function checkCtrStateAndCity() {
        if (checkIfSelectInputIsValid("Country") && checkIfSelectInputIsValid("State") && checkIfSelectInputIsValid("City")) {
            $("#address-validate").removeClass('alert-validate')
        } else {
            $("#address-validate").addClass('alert-validate')
        }
    }
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

    // Update States Options When Country Has Been Changed.
    $scope.updateStatesOptions = (newValCountry) => {
        dataService.allCountryStates = ["Choose State"]
        if (checkIfSelectInputIsValid("Country")) {
            $http.get(globalConst.getStatesBasicUrl + `${newValCountry}`)
                .then(function (resp) {
                    const temp = resp.data.details.regionalBlocs;
                    if (temp.length > 0) {
                        $.each(temp, function (i, state) {
                            dataService.allCountryStates.push(state.state_name)
                        })
                    } else {
                        dataService.allCountryStates.push(`No States Found For The Selected Country.`)
                    }
                })
                .catch(function (err) {
                    alert('alert')
                })
                .finally(function () {
                    $scope.countryStates = dataService.allCountryStates
                });
        }
    }


    // Update States Options When Country Has Been Changed.
    $scope.updateStatesOptions = (newValCountry) => {
        dataService.allCountryStates = ["Choose State"]
        if (checkIfSelectInputIsValid("Country")) {
            $http.get("api/search/statesOrCities?url=" + `${globalConst.getStatesBasicUrl}` + `${newValCountry}`)
                .then(function (resp) {
                    const temp = JSON.parse(resp.data).details.regionalBlocs;
                    if (temp.length > 0) {
                        $.each(temp, function (i, state) {
                            dataService.allCountryStates.push(state.state_name)
                        })
                    } else {
                        dataService.allCountryStates.push(`No States Found For The Selected Country.`)
                    }
                })
                .catch(function (err) {
                    alert('alert')
                })
                .finally(function () {
                    $scope.countryStates = dataService.allCountryStates
                });
        }
    }

    // Update Cities Options When State Has Been Changed.
    $scope.updateCitiesOptions = (country, newValState) => {
        dataService.allStateCities = ["Choose City"]
        if (checkIfSelectInputIsValid("State")) {
            $http.get(`api/search/statesOrCities?url=${globalConst.getStatesBasicUrl}${country}&state=${newValState}`)
                .then(function (resp) {
                    const temp = JSON.parse(resp.data)
                            $.each(temp, function (i, city) {
                            dataService.allStateCities.push(city.city_name)
                        })
                    dataService.allStateCities.pop()
                    if (dataService.allStateCities.length < 2) {
                        dataService.allStateCities.push(`No Cities Found For The Selected State.`)
                    }
                })
                .catch(function (err) {
                    alert('alert')
                })
                .finally(function () {
                    $scope.stateCities = dataService.allStateCities
                });
        }
    }

    // Add/Remove Error-Alert When Try To Sign-Up
    $scope.$watch('tryWhenInvalid', (newVal) => {
        if (newVal) {
            if (!$scope.regForm.name.$valid)
                $("#fullname-validate").addClass('alert-validate')
            if (!$scope.regForm.email.$valid)
                $("#email-validate").addClass('alert-validate')
            if (!checkIfSelectInputIsValid("Country") || !checkIfSelectInputIsValid("State") || !checkIfSelectInputIsValid("City"))
                $("#address-validate").addClass('alert-validate')
            if (!$scope.regForm.phNumber.$valid)
                $("#ph-number-validate").addClass('alert-validate')
            if (!$scope.regForm.pass.$valid)
                $("#pass-validate").addClass('alert-validate')
            if (!$scope.regForm.termsAgree.checked)
                $("#agree-validate").addClass('alert-validate-agree')
        } else {
            $("#fullname-validate").removeClass('alert-validate')
            $("#email-validate").removeClass('alert-validate')
            $("#address-validate").removeClass('alert-validate')
            $("#ph-number-validate").removeClass('alert-validate')
            $("#pass-validate").removeClass('alert-validate')
            $("#agree-validate").removeClass('alert-validate-agree')
        }
    })

    // Function That Checked If The Select Inputs Is Valid
    function checkIfSelectInputIsValid(input) {
        switch (input.toLowerCase()) {
            case "country":
                return $scope.form.address.country != "Choose Country"
            case "state":
                return ($scope.form.address.state != undefined && $scope.form.address.state != "Choose State" && $scope.form.address.state != "No States Found For The Selected Country.")
            case "city":
                return ($scope.form.address.city != undefined && $scope.form.address.city != "Choose City" && $scope.form.address.city != "No Cities Found For The Selected State.")
            default:
                return true;
        }
    }
}