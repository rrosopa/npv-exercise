$(document).ready(function () {
    var _elements = {
        buttonAddCashflow: $('#btn-add-cashflow'),
        buttonCalculate: $('#btn-calculate'),
        divCashflow: $('#div-cashflows'),
        divError: $('#div-error'),
        divPrevious: $('#div-previous'),
        divResult: $('#div-result'),       
        formNpv: $('#form-npv')
    };

    var _cashflowTemplate = `
                    <div class="mt-2 d-flex align-items-center justify-content-center">
                        <input type="number"
                               name="cashflows"
                               class="form-control input-cashflow"
                               pattern="^-?([0-9]+|[0-9]{1,3}(,[0-9]{3})*)(\.[0-9]{1,2})?$"
                               required />
                        <button type="button"
                                class="btn btn-danger ml-2 btn-remove font-weight-bolder">
                            X
                        </button>
                    </div>`;

    var _resultTemplate = `<div class="overflow-auto">
                            <table class="table">
                             <tr>
                                <th class="text-center">Cashflows</th>
                                <th class="text-center">Initial Value</th>
                                <th class="text-center">Discount Rate</th>
                                <th class="text-center">NPV</th>
                                <th class="text-center">PV of Expected Cash flows</th>
                             </tr>
                             [CONTENT]
                            </table>
                           </div>`;

    function removeCashflow(e) {
        $(e.currentTarget.parentElement).remove();
        if ($('input[name=cashflows]').length < 12) {
            _elements.buttonAddCashflow.show();
        }        
    }

    function isFormValid() {
        _elements.divError.empty();

        var form = _elements.formNpv[0];
        if (!form.checkValidity())
            return false;

        var errorCtr = 0;
        var initialValue = Number(form['initialValue'].value);
        if (isNaN(initialValue) || initialValue > 0) {
            _elements.divError.append('<li>Initial value should be a number less than or equal to 0</li>');
            errorCtr++;
        }

        var lowerBoundRate = Number(form['lowerBoundRate'].value);
        if (isNaN(lowerBoundRate) ||lowerBoundRate < 0.0001 || lowerBoundRate > 100) {
            _elements.divError.append('<li>Lower bound rate should be a number greater than 0.0001 and less than 100</li>');
            errorCtr++;
        }

        var upperBoundRate = Number(form['upperBoundRate'].value);
        if (isNaN(upperBoundRate) ||upperBoundRate < 0.0001 || upperBoundRate > 100) {
            _elements.divError.append('<li>Upper bound rate should be a number greater than 0.0001 and less than 100</li>');
            errorCtr++;
        }

        var increment = Number(form['increment'].value);
        if (isNaN(increment) ||increment < 0.0001 || increment > 100) {
            _elements.divError.append('<li>Increment should be a number greater than 0.0001 and less than 100</li>');
            errorCtr++;
        }

        return errorCtr == 0;
    }

    function onCalculateSuccess(result) {
        if (result.hasErrors) {
            result.errors.forEach(error => {
                _elements.divResult.append(`<p>${error.message}</p>`);
            });
        }
        else {
            var contentString = '';

            result.data.forEach(x => {
                if (x.hasErrors) {
                    contentString += `<tr><td class="text-center" colspan="5">${x.errors[0].message}</td></tr>`;
                }
                else {
                    contentString += `<tr>
                                            <td class="text-right">${x.cashflows.join(', ')}</td>
                                            <td class="text-right">${x.initialValue}</td>
                                            <td class="text-right">${x.rate}</td>
                                            <td class="text-right">${x.netPresentValue}</td>
                                            <td class="text-right">${x.presentValueOfExpectedCashflows}</td>
                                            </tr>`;
                }
            });

            _elements.divResult.append(_resultTemplate.replace('[CONTENT]', contentString));
            getPreviousCalculations();
        }
    }

    function getPreviousCalculations() {
        _elements.divPrevious.empty();

        $.get(
            '/api/npvVariables',
            function (result) {
                if (result.hasErrors) {
                    result.errors.forEach(error => {
                        _elements.divPrevious.append(`<p>${error.message}</p>`);
                    });
                }
                else {

                    if (result.data.length == 0) {
                        _elements.divPrevious.append('<p>no calculations has been made yet.</p>');
                    }
                    else {
                        var contentString = '';
                        result.data.forEach(x => {
                            var cashflowString = '';
                            x.cashflows.forEach(cashflow => cashflowString += `${cashflow.cashflow}, `);

                            contentString += `<div class="row mb-5">
                                        <div class="col-6">
                                            <div class="input-group d-flex justify-content-between">
                                                <label class="font-weight-bold">Initial Value:</label><span>${x.initialValue}</span>
                                            </div>
                                            <div class="input-group d-flex flex-column">
                                                <label class="font-weight-bold">Cashflows:</label><span>${cashflowString}</span>
                                            </div>      
                                        </div>
                                        <div class="col-6">
                                            <div class="input-group d-flex justify-content-between">
                                                <label class="font-weight-bold">Lower bound rate:</label><span>${x.lowerBoundRate}</span>
                                            </div>
                                            <div class="input-group d-flex justify-content-between">
                                                <label class="font-weight-bold">Upper bound rate:</label><span>${x.upperBoundRate}</span>
                                            </div>
                                            <div class="input-group d-flex justify-content-between">
                                                <label class="font-weight-bold">increment:</label><span>${x.increment}</span>
                                            </div>
                                        </div>
                                        <div class="col-12 d-flex justify-content-center">
                                            <button type="button" class="btn btn-primary recalculate" data-id="${x.id}">Recalculate</button>
                                        </div>
                                      </div>`;
                        });

                        _elements.divPrevious.append(contentString);

                        $('button.recalculate').click(function (e) {
                            _elements.divResult.empty();

                            $.get(
                                `/api/npv/${e.currentTarget.dataset.id}`,
                                onCalculateSuccess
                            ).fail(function (error) {
                                console.log(error);
                                _elements.divResult.append('<p>An error occured while processing your request</p>');
                            });
                        });
                    }                    
                }
            }
        ).fail(function (error) {
            console.log(error);
            _elements.divPrevious.append('<p>An error occured while trying to get previous results</p>');
        });
    }

    _elements.buttonCalculate.click(function (e) {
        e.preventDefault();
        
        if (isFormValid()) {
            _elements.divResult.empty();

            $.get(
                `/api/npv?${_elements.formNpv.serialize()}`,
                onCalculateSuccess
            ).fail(function (error) {
                console.log(error);
                _elements.divResult.append('<p>An error occured while processing your request</p>');
            });
        }
        else {
            _elements.formNpv[0].reportValidity();
        }
    });

    _elements.buttonAddCashflow.click(function () {
        _elements.divCashflow.append(_cashflowTemplate);
        $('.btn-remove').last().click(removeCashflow);

        if ($('input[name=cashflows]').length >= 12) {
            _elements.buttonAddCashflow.hide();
        }
    });    

    $('.btn-remove').click(removeCashflow);

    getPreviousCalculations();
});