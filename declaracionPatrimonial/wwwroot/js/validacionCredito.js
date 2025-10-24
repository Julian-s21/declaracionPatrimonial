document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("formCreditoBancario");
    const errorSummary = document.getElementById("errorSummary");
    const selectCuenta = document.getElementById("IDCuentaBancaria");
    const inputSaldo = document.getElementById("saldoCuenta");
    const inputNumero = document.getElementById("numeroCuenta");
    const selectBanco = document.getElementById("IDBanco");
    const selectTipoCuenta = document.getElementById("IDtipoCuenta");
    const hiddenBanco = document.getElementById("hiddenBanco");
    const hiddenTipoCuenta = document.getElementById("hiddenTipoCuenta");
    const inputCantidad = form.querySelector("[name='Cantidad_AprobadaCredito']");
    const inputMotivo = form.querySelector("[name='Motivo_creditoBancario']");
    const btnCancelar = document.getElementById("btnCancelar");
    const confirmModalEl = document.getElementById("confirmModal");
    const cancelModalEl = document.getElementById("cancelModal");

    form.setAttribute("novalidate", true);

    // ===============================
    // MODALES
    // ===============================
    const cancelModal = cancelModalEl ? new bootstrap.Modal(cancelModalEl) : null;
    if (btnCancelar && cancelModal) {
        btnCancelar.addEventListener("click", () => cancelModal.show());
    }

    // ===============================
    // BLOQUEAR CARACTERES ESPECIALES EN MOTIVO
    // ===============================
    inputMotivo.addEventListener("keypress", e => {
        const regex = /^[a-zA-Z0-9ÁÉÍÓÚáéíóúñÑ\s.,-]$/;
        if (!regex.test(e.key)) {
            e.preventDefault();
            showError(inputMotivo, "No se permiten caracteres especiales.");
        }
    });

    // ===============================
    // CAMBIO DE CUENTA BANCARIA
    // ===============================
    selectCuenta.addEventListener("change", () => {
        const selected = selectCuenta.selectedOptions[0];
        if (!selected || selected.value === "") {
            resetCuenta();
            showError(selectCuenta, "Debe seleccionar una cuenta bancaria válida.");
            return;
        }
        inputSaldo.value = selected.dataset.saldo || "";
        inputNumero.value = selected.dataset.numero || "";
        selectBanco.value = selected.dataset.banco || "";
        selectTipoCuenta.value = selected.dataset.tipocuenta || "";
        hiddenBanco.value = selected.dataset.banco || "";
        hiddenTipoCuenta.value = selected.dataset.tipocuenta || "";

        markValid(selectCuenta);
        clearError(selectCuenta);
    });

    // ===============================
    // VALIDACIONES EN TIEMPO REAL
    // ===============================
    inputCantidad.addEventListener("input", () => {
        const cantidad = parseFloat(inputCantidad.value.replace(",", ""));
        if (isNaN(cantidad) || cantidad <= 0) {
            showError(inputCantidad, "Ingrese una cantidad válida mayor a Q0.00.");
        } else if (cantidad < 100) {
            showError(inputCantidad, "El monto mínimo permitido es Q100.00.");
        } else if (cantidad > 1000000) {
            showError(inputCantidad, "El monto máximo permitido es Q1,000,000.00.");
        } else {
            markValid(inputCantidad);
            clearError(inputCantidad);
        }
    });

    inputMotivo.addEventListener("input", () => {
        const value = inputMotivo.value.trim();
        const regex = /^[a-zA-Z0-9ÁÉÍÓÚáéíóúñÑ\s.,-]+$/;
        if (value.length === 0) {
            showError(inputMotivo, "Este campo es obligatorio. Ingrese el motivo del crédito.");
        } else if (value.length < 5) {
            showError(inputMotivo, "El motivo es demasiado corto (mínimo 5 caracteres).");
        } else if (!regex.test(value)) {
            showError(inputMotivo, "El motivo contiene caracteres no permitidos.");
        } else {
            markValid(inputMotivo);
            clearError(inputMotivo);
        }
    });

    // ===============================
    // VALIDACIÓN AL ENVIAR FORMULARIO
    // ===============================
    function validarFormulario() {
        limpiarResumen();
        let errores = [];
        const cantidad = parseFloat(inputCantidad.value.replace(",", "")) || 0;
        const saldo = parseFloat(inputSaldo.value.replace(",", "")) || 0;
        const motivo = inputMotivo.value.trim();
        const regexMotivo = /^[a-zA-Z0-9ÁÉÍÓÚáéíóúñÑ\s.,-]+$/;

        // Cuenta
        if (!selectCuenta.value) {
            errores.push("Debe seleccionar una cuenta bancaria.");
            showError(selectCuenta, "Debe seleccionar una cuenta bancaria válida.");
        } else clearError(selectCuenta);

        // Cantidad
        if (isNaN(cantidad) || cantidad < 100) {
            errores.push("Cantidad inválida (mínimo Q100.00).");
            showError(inputCantidad, "Ingrese una cantidad válida (mínimo Q100.00).");
        } else if (cantidad > 1000000) {
            errores.push("La cantidad supera el máximo permitido (Q1,000,000.00).");
            showError(inputCantidad, "La cantidad supera el máximo permitido.");
        } else if (saldo && cantidad > saldo * 10) {
            errores.push("La cantidad no guarda relación con el saldo actual.");
            showError(inputCantidad, "El monto aprobado no guarda relación con el saldo.");
        } else clearError(inputCantidad);

        // Motivo
        if (!motivo || motivo.length < 5) {
            errores.push("Motivo inválido (mínimo 5 caracteres).");
            showError(inputMotivo, "Debe indicar un motivo válido (mínimo 5 caracteres).");
        } else if (!regexMotivo.test(motivo)) {
            errores.push("Motivo contiene caracteres no permitidos.");
            showError(inputMotivo, "El motivo contiene caracteres no permitidos.");
        } else clearError(inputMotivo);

        if (errores.length > 0) {
            errorSummary.classList.remove("d-none");
            errorSummary.innerHTML = `
                <strong>Se encontraron los siguientes errores:</strong>
                <ul class="mb-0 mt-2">
                    ${errores.map(e => `<li>${e}</li>`).join('')}
                </ul>
            `;
            errorSummary.scrollIntoView({ behavior: "smooth", block: "start" });
            return false;
        }

        return true;
    }

    form.addEventListener("submit", function (e) {
        e.preventDefault();
        if (validarFormulario()) {
            const confirmModal = new bootstrap.Modal(confirmModalEl);
            confirmModal.show();

            confirmModalEl.addEventListener("hidden.bs.modal", () => {
                form.submit();
            }, { once: true });
        }
    });

    // ===============================
    // EFECTO VISUAL DE INPUTS
    // ===============================
    document.querySelectorAll('input, select, textarea').forEach(el => {
        el.addEventListener('input', () => {
            if (el.checkValidity()) {
                el.classList.remove('is-invalid');
                el.classList.add('is-valid');
            }
        });
    });

    // ===============================
    // EFECTO RIPPLE EN BOTONES
    // ===============================
    document.querySelectorAll('.ripple').forEach(btn => {
        btn.addEventListener('click', function (e) {
            const circle = document.createElement('span');
            const diameter = Math.max(this.clientWidth, this.clientHeight);
            const rect = this.getBoundingClientRect();
            circle.style.width = circle.style.height = `${diameter}px`;
            circle.style.left = `${e.clientX - rect.left - diameter / 2}px`;
            circle.style.top = `${e.clientY - rect.top - diameter / 2}px`;
            circle.classList.add('ripple-effect');
            const ripple = this.getElementsByClassName('ripple-effect')[0];
            if (ripple) ripple.remove();
            this.appendChild(circle);
        });
    });

    // ===============================
    // FUNCIONES AUXILIARES
    // ===============================
    function showError(element, message) {
        element.classList.remove("is-valid");
        element.classList.add("is-invalid");
        let feedback = element.parentElement.querySelector(".invalid-feedback");
        if (!feedback) {
            feedback = document.createElement("div");
            feedback.classList.add("invalid-feedback");
            element.parentElement.appendChild(feedback);
        }
        feedback.textContent = message;
    }

    function clearError(element) {
        element.classList.remove("is-invalid");
        element.classList.add("is-valid");
        const feedback = element.parentElement.querySelector(".invalid-feedback");
        if (feedback) feedback.remove();
    }

    function markValid(element) {
        element.classList.remove("is-invalid");
        element.classList.add("is-valid");
    }

    function resetCuenta() {
        inputSaldo.value = "";
        inputNumero.value = "";
        selectBanco.value = "";
        selectTipoCuenta.value = "";
        hiddenBanco.value = "";
        hiddenTipoCuenta.value = "";
        clearError(selectCuenta);
    }

    function limpiarResumen() {
        errorSummary.classList.add("d-none");
        errorSummary.innerHTML = "";
    }
});
