// ===============================
// VALIDACIÓN DE CUENTAS BANCARIAS
// ===============================

document.addEventListener("DOMContentLoaded", function () {

    const form = document.getElementById("formCuentaBancaria");
    const errorSummary = document.getElementById("errorSummary");
    const btnCancelar = document.getElementById("btnCancelar");
    const confirmModal = document.getElementById("confirmModal");
    const cancelModal = document.getElementById("cancelModal");

    if (!form) return;

    // ===============================
    // DESACTIVAR VALIDACIÓN HTML5
    // ===============================
    form.setAttribute("novalidate", true);
    form.querySelectorAll("input, select, textarea").forEach(campo => {
        campo.addEventListener("invalid", e => e.preventDefault());
    });

    // ===============================
    // REFERENCIAS A CAMPOS
    // ===============================
    const inputNumeroCuenta = document.querySelector("[name='Numero_cuentaBancaria']");
    const inputSaldo = document.querySelector("[name='Saldo_cuentaBancaria']");
    const selectBanco = document.querySelector("[name='IDBanco']");
    const selectTipoCuenta = document.querySelector("[name='IDTipoCuenta']");
    const inputDescripcion = document.querySelector("[name='Descripcion_cuentaBancaria']");
    const inputMoneda = document.querySelector("[name='TipoMoneda']");
    const inputFechaApertura = document.querySelector("[name='Fecha_aperturaCuenta']");

    // ===============================
    // FUNCIONES AUXILIARES
    // ===============================
    function showError(element, message) {
        element.classList.remove("is-valid");
        element.classList.add("is-invalid");

        let feedback = element.parentElement.querySelector(".invalid-feedback");
        if (!feedback) {
            feedback = document.createElement("div");
            feedback.classList.add("invalid-feedback", "d-block", "small");
            element.parentElement.appendChild(feedback);
        }
        feedback.textContent = message;
    }

    function clearError(element) {
        element.classList.remove("is-invalid");
        element.classList.add("is-valid");
        const feedback = element.parentElement.querySelector(".invalid-feedback");
        if (feedback) feedback.textContent = "";
    }

    function markValid(element) {
        element.classList.remove("is-invalid");
        element.classList.add("is-valid");
    }

    // ===============================
    // VALIDACIONES EN TIEMPO REAL
    // ===============================
    const regexTexto = /^[a-zA-ZÁÉÍÓÚáéíóúñÑ0-9\s.,-]+$/;
    const regexNumeroCuenta = /^[0-9]{6,20}$/;

    inputNumeroCuenta.addEventListener("input", () => {
        const valor = inputNumeroCuenta.value.trim();
        if (valor === "") {
            showError(inputNumeroCuenta, "Debe ingresar el número de cuenta.");
        } else if (!regexNumeroCuenta.test(valor)) {
            showError(inputNumeroCuenta, "El número de cuenta debe tener entre 6 y 20 dígitos numéricos.");
        } else {
            markValid(inputNumeroCuenta);
            clearError(inputNumeroCuenta);
        }
    });

    selectBanco.addEventListener("change", () => {
        if (selectBanco.value === "") {
            showError(selectBanco, "Debe seleccionar un banco asociado.");
        } else {
            markValid(selectBanco);
            clearError(selectBanco);
        }
    });

    selectTipoCuenta.addEventListener("change", () => {
        if (selectTipoCuenta.value === "") {
            showError(selectTipoCuenta, "Debe seleccionar un tipo de cuenta.");
        } else {
            markValid(selectTipoCuenta);
            clearError(selectTipoCuenta);
        }
    });

    inputSaldo.addEventListener("input", () => {
        const valor = parseFloat(inputSaldo.value);
        if (isNaN(valor) || valor < 0) {
            showError(inputSaldo, "Ingrese un saldo válido (no puede ser negativo).");
        } else if (valor > 100000000) {
            showError(inputSaldo, "El saldo no puede exceder Q100,000,000.00.");
        } else {
            markValid(inputSaldo);
            clearError(inputSaldo);
        }
    });

    inputDescripcion.addEventListener("input", () => {
        const valor = inputDescripcion.value.trim();
        if (valor === "") {
            showError(inputDescripcion, "Debe ingresar una descripción de la cuenta.");
        } else if (valor.length < 5) {
            showError(inputDescripcion, "La descripción es demasiado corta (mínimo 5 caracteres).");
        } else if (valor.length > 200) {
            showError(inputDescripcion, "La descripción es demasiado larga (máximo 200 caracteres).");
        } else if (!regexTexto.test(valor)) {
            showError(inputDescripcion, "La descripción contiene caracteres no permitidos.");
        } else {
            markValid(inputDescripcion);
            clearError(inputDescripcion);
        }
    });

    inputMoneda.addEventListener("input", () => {
        const valor = inputMoneda.value.trim();
        if (valor === "") {
            showError(inputMoneda, "Debe especificar el tipo de moneda (Ej. Quetzales o Dólares).");
        } else if (!regexTexto.test(valor)) {
            showError(inputMoneda, "El tipo de moneda contiene caracteres no permitidos.");
        } else {
            markValid(inputMoneda);
            clearError(inputMoneda);
        }
    });

    inputFechaApertura.addEventListener("change", () => {
        const fecha = new Date(inputFechaApertura.value);
        const hoy = new Date();
        if (!inputFechaApertura.value) {
            showError(inputFechaApertura, "Debe ingresar la fecha de apertura de la cuenta.");
        } else if (fecha > hoy) {
            showError(inputFechaApertura, "La fecha de apertura no puede ser futura.");
        } else {
            markValid(inputFechaApertura);
            clearError(inputFechaApertura);
        }
    });

    // ===============================
    // VALIDACIÓN FINAL (SUBMIT)
    // ===============================
    form.addEventListener("submit", function (event) {
        event.preventDefault();
        let errores = [];

        // --- Número de cuenta ---
        const numero = inputNumeroCuenta.value.trim();
        if (numero === "") {
            errores.push("Debe ingresar el número de cuenta.");
            showError(inputNumeroCuenta, "Debe ingresar el número de cuenta.");
        } else if (!regexNumeroCuenta.test(numero)) {
            errores.push("El número de cuenta debe tener entre 6 y 20 dígitos numéricos.");
            showError(inputNumeroCuenta, "El número de cuenta debe tener entre 6 y 20 dígitos numéricos.");
        }

        // --- Banco ---
        if (selectBanco.value === "") {
            errores.push("Debe seleccionar un banco asociado.");
            showError(selectBanco, "Debe seleccionar un banco asociado.");
        }

        // --- Tipo de cuenta ---
        if (selectTipoCuenta.value === "") {
            errores.push("Debe seleccionar un tipo de cuenta.");
            showError(selectTipoCuenta, "Debe seleccionar un tipo de cuenta.");
        }

        // --- Saldo ---
        const saldo = parseFloat(inputSaldo.value);
        if (isNaN(saldo) || saldo < 0) {
            errores.push("Debe ingresar un saldo válido (no puede ser negativo).");
            showError(inputSaldo, "Debe ingresar un saldo válido (no puede ser negativo).");
        } else if (saldo > 100000000) {
            errores.push("El saldo no puede exceder Q100,000,000.00.");
            showError(inputSaldo, "El saldo no puede exceder Q100,000,000.00.");
        }

        // --- Descripción ---
        const descripcion = inputDescripcion.value.trim();
        if (descripcion === "" || descripcion.length < 5) {
            errores.push("Debe ingresar una descripción válida (mínimo 5 caracteres).");
            showError(inputDescripcion, "Debe ingresar una descripción válida (mínimo 5 caracteres).");
        }

        // --- Tipo de moneda ---
        const moneda = inputMoneda.value.trim();
        if (moneda === "") {
            errores.push("Debe especificar el tipo de moneda.");
            showError(inputMoneda, "Debe especificar el tipo de moneda.");
        }

        // --- Fecha de apertura ---
        const fecha = new Date(inputFechaApertura.value);
        if (!inputFechaApertura.value) {
            errores.push("Debe ingresar la fecha de apertura.");
            showError(inputFechaApertura, "Debe ingresar la fecha de apertura.");
        } else if (fecha > new Date()) {
            errores.push("La fecha de apertura no puede ser futura.");
            showError(inputFechaApertura, "La fecha de apertura no puede ser futura.");
        }

        // --- Mostrar errores o enviar ---
        if (errores.length > 0) {
            if (errorSummary) {
                errorSummary.classList.remove("d-none");
                errorSummary.innerHTML = `
                    <strong>Se encontraron los siguientes errores:</strong>
                    <ul class="mb-0 mt-2">${errores.map(e => `<li>${e}</li>`).join("")}</ul>
                `;
            }
            window.scrollTo({ top: 0, behavior: "smooth" });
            return;
        }

        if (errorSummary) errorSummary.classList.add("d-none");

        // Mostrar modal de confirmación
        const modal = new bootstrap.Modal(confirmModal);
        modal.show();

        setTimeout(() => form.submit(), 700);
    });

    // ===============================
    // MODAL DE CANCELACIÓN
    // ===============================
    if (btnCancelar && cancelModal) {
        btnCancelar.addEventListener("click", function () {
            const modal = new bootstrap.Modal(cancelModal);
            modal.show();
        });
    }

    // ===============================
    // EFECTO RIPPLE EN BOTONES
    // ===============================
    document.querySelectorAll(".ripple").forEach(button => {
        button.addEventListener("click", function (e) {
            const circle = document.createElement("span");
            const diameter = Math.max(this.clientWidth, this.clientHeight);
            const rect = this.getBoundingClientRect();

            circle.style.width = circle.style.height = `${diameter}px`;
            circle.style.left = `${e.clientX - rect.left - diameter / 2}px`;
            circle.style.top = `${e.clientY - rect.top - diameter / 2}px`;
            circle.classList.add("ripple-effect");

            const ripple = this.getElementsByClassName("ripple-effect")[0];
            if (ripple) ripple.remove();

            this.appendChild(circle);
        });
    });
});
