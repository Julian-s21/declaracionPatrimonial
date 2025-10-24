// ========================================
// VALIDACIONES PERSONALIZADAS - OTRO PASIVO
// ========================================
document.addEventListener("DOMContentLoaded", function () {

    // Buscar el formulario (crear o editar)
    const form = document.getElementById("formOtroPasivo") || 
                 document.getElementById("formEditarPasivo") || 
                 document.querySelector("form.needs-validation");
    if (!form) return;

    // Desactivar validaciones nativas del navegador
    form.setAttribute("novalidate", true);

    const campos = {
        IDCuentaBancaria: form.querySelector("#IDCuentaBancaria"),
        Cantidad_aprobadaPasivo: form.querySelector("[name='Cantidad_aprobadaPasivo']"),
        Motivo_Pasivo: form.querySelector("[name='Motivo_Pasivo']")
    };

    // Crear contenedor general de errores (resumen)
    let errorSummary = document.createElement("div");
    errorSummary.id = "errorSummary";
    errorSummary.className = "alert alert-danger mt-4 d-none rounded-4 shadow-sm";
    errorSummary.innerHTML = `
        <h6 class='fw-bold'>
            <i class='bi bi-exclamation-triangle me-2'></i>
            Errores encontrados:
        </h6>
        <ul class='mb-0'></ul>`;
    form.appendChild(errorSummary);

    // ===============================
    // FUNCIONES AUXILIARES
    // ===============================
    function mostrarError(input, mensaje) {
        input.classList.add("is-invalid");
        input.classList.remove("is-valid");

        let feedback = input.parentElement.querySelector(".invalid-feedback");
        if (!feedback) {
            feedback = document.createElement("div");
            feedback.classList.add("invalid-feedback", "d-block", "small");
            input.parentElement.appendChild(feedback);
        }
        feedback.textContent = mensaje;
    }

    function limpiarError(input) {
        input.classList.remove("is-invalid");
        input.classList.add("is-valid");
        const feedback = input.parentElement.querySelector(".invalid-feedback");
        if (feedback) feedback.remove();
    }

    function agregarErrorResumen(mensaje) {
        const ul = errorSummary.querySelector("ul");
        const li = document.createElement("li");
        li.textContent = mensaje;
        ul.appendChild(li);
    }

    function limpiarErrorResumen() {
        errorSummary.classList.add("d-none");
        errorSummary.querySelector("ul").innerHTML = "";
    }

    // ===============================
    // VALIDACIONES EN TIEMPO REAL
    // ===============================

    // Cuenta bancaria
    campos.IDCuentaBancaria.addEventListener("change", function () {
        if (this.value.trim() === "") {
            mostrarError(this, "Debe seleccionar una cuenta bancaria asociada.");
        } else {
            limpiarError(this);
        }
    });

    // Cantidad aprobada
    campos.Cantidad_aprobadaPasivo.addEventListener("input", function () {
        const valor = parseFloat(this.value);
        if (this.value.trim() === "") {
            mostrarError(this, "Debe ingresar una cantidad.");
        } else if (isNaN(valor)) {
            mostrarError(this, "El valor ingresado no es válido.");
        } else if (valor <= 0) {
            mostrarError(this, "Ingrese una cantidad válida mayor que 0.");
        } else if (valor > 10000000) {
            mostrarError(this, "La cantidad no puede superar Q10,000,000.");
        } else {
            limpiarError(this);
        }
    });

    // Motivo del pasivo
    campos.Motivo_Pasivo.addEventListener("input", function () {
        const valor = this.value.trim();
        if (valor === "") {
            mostrarError(this, "Debe ingresar el motivo del pasivo.");
        } else if (valor.length < 5) {
            mostrarError(this, "El motivo debe tener al menos 5 caracteres.");
        } else if (/[^a-zA-ZáéíóúÁÉÍÓÚñÑ0-9\s,.]/.test(valor)) {
            mostrarError(this, "El motivo contiene caracteres no válidos.");
        } else {
            limpiarError(this);
        }
    });

    // ===============================
    // VALIDACIÓN GENERAL AL ENVIAR
    // ===============================
    form.addEventListener("submit", function (e) {
        limpiarErrorResumen();
        let hayErrores = false;

        // Validar cuenta bancaria
        if (campos.IDCuentaBancaria.value.trim() === "") {
            hayErrores = true;
            mostrarError(campos.IDCuentaBancaria, "Debe seleccionar una cuenta bancaria asociada.");
            agregarErrorResumen("Seleccione una cuenta bancaria asociada.");
        }

        // Validar cantidad
        const valor = parseFloat(campos.Cantidad_aprobadaPasivo.value);
        if (campos.Cantidad_aprobadaPasivo.value.trim() === "") {
            hayErrores = true;
            mostrarError(campos.Cantidad_aprobadaPasivo, "Debe ingresar una cantidad.");
            agregarErrorResumen("Debe ingresar una cantidad.");
        } else if (isNaN(valor)) {
            hayErrores = true;
            mostrarError(campos.Cantidad_aprobadaPasivo, "El valor ingresado no es válido.");
            agregarErrorResumen("El valor ingresado no es válido.");
        } else if (valor <= 0) {
            hayErrores = true;
            mostrarError(campos.Cantidad_aprobadaPasivo, "Ingrese una cantidad válida mayor que 0.");
            agregarErrorResumen("Ingrese una cantidad válida mayor que 0.");
        } else if (valor > 10000000) {
            hayErrores = true;
            mostrarError(campos.Cantidad_aprobadaPasivo, "La cantidad no puede superar Q10,000,000.");
            agregarErrorResumen("La cantidad no puede superar Q10,000,000.");
        }

        // Validar motivo del pasivo
        const motivo = campos.Motivo_Pasivo.value.trim();
        if (motivo === "") {
            hayErrores = true;
            mostrarError(campos.Motivo_Pasivo, "Debe ingresar el motivo del pasivo.");
            agregarErrorResumen("Debe ingresar el motivo del pasivo.");
        } else if (motivo.length < 5) {
            hayErrores = true;
            mostrarError(campos.Motivo_Pasivo, "El motivo debe tener al menos 5 caracteres.");
            agregarErrorResumen("El motivo debe tener al menos 5 caracteres.");
        } else if (/[^a-zA-ZáéíóúÁÉÍÓÚñÑ0-9\s,.]/.test(motivo)) {
            hayErrores = true;
            mostrarError(campos.Motivo_Pasivo, "El motivo contiene caracteres no válidos.");
            agregarErrorResumen("El motivo contiene caracteres no válidos.");
        }

        // Mostrar errores o enviar formulario
        if (hayErrores) {
            e.preventDefault();
            errorSummary.classList.remove("d-none");
            errorSummary.scrollIntoView({ behavior: "smooth", block: "start" });
        } else {
            errorSummary.classList.add("d-none");
        }
    });

    // ===============================
    // EFECTO RIPPLE (Botones)
    // ===============================
    document.querySelectorAll('.ripple').forEach(btn => {
        btn.addEventListener('click', function (e) {
            const ripple = document.createElement('span');
            ripple.classList.add('ripple-effect');
            ripple.style.left = `${e.offsetX}px`;
            ripple.style.top = `${e.offsetY}px`;
            this.appendChild(ripple);
            setTimeout(() => ripple.remove(), 600);
        });
    });
});
