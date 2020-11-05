module.value("dataService", {

    tokenUserName: "ADMIN",
    tokenPassword: "9999",

    //matchingVacancyFlightsUrl: `<%: Url.Action("byfilters", "flights", "search", "api", new { onlyVacancy= "true"}) %>`,
    matchingVacancyFlightsUrl: `api/search/flights/byfilters?onlyVacancy=true`,
    allCountries: [],
    allCompanies: [],
    matchingVacancyFlights: [],

    allCountryStates: [],
    allStateCities: []
})