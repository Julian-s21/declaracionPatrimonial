// ===============================
// VALIDACIÓN DE BIENES PATRIMONIALES
// ===============================

document.addEventListener("DOMContentLoaded", function () {

    const form = document.getElementById("formBien");
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
    const inputNombre = document.querySelector("[name='Nombre_Bienes']");
    const inputFecha = document.querySelector("[name='Fecha_adquisicionBien']");
    const inputDescripcion = document.querySelector("[name='Descripcion_Bien']");
    const inputForma = document.querySelector("[name='Forma_adquisicionBien']");
    const inputPrecio = document.querySelector("[name='Precio_Bien']");
    const selectTipoInmueble = document.querySelector("[name='IDtipoInmueble']");
    const selectTipoPropiedad = document.querySelector("[name='IDtipoPropiedad']");

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

    inputNombre.addEventListener("input", () => {
        const valor = inputNombre.value.trim();
        if (valor.length === 0) {
            showError(inputNombre, "Debe ingresar el nombre del bien.");
        } else if (valor.length < 3) {
            showError(inputNombre, "El nombre es demasiado corto (mínimo 3 caracteres).");
        } else if (!regexTexto.test(valor)) {
            showError(inputNombre, "El nombre contiene caracteres no permitidos.");
        } else {
            markValid(inputNombre);
            clearError(inputNombre);
        }
    });

    inputDescripcion.addEventListener("input", () => {
        const valor = inputDescripcion.value.trim();
        if (valor.length === 0) {
            showError(inputDescripcion, "Debe ingresar una descripción del bien.");
        } else if (valor.length < 10) {
            showError(inputDescripcion, "La descripción es demasiado corta (mínimo 10 caracteres).");
        } else if (valor.length > 500) {
            showError(inputDescripcion, "La descripción es demasiado larga (máximo 500 caracteres).");
        } else if (!regexTexto.test(valor)) {
            showError(inputDescripcion, "La descripción contiene caracteres no permitidos.");
        } else {
            markValid(inputDescripcion);
            clearError(inputDescripcion);
        }
    });

    inputForma.addEventListener("input", () => {
        const valor = inputForma.value.trim();
        if (valor.length === 0) {
            showError(inputForma, "Debe ingresar la forma de adquisición.");
        } else if (!regexTexto.test(valor)) {
            showError(inputForma, "La forma de adquisición contiene caracteres no permitidos.");
        } else {
            markValid(inputForma);
            clearError(inputForma);
        }
    });

    inputPrecio.addEventListener("input", () => {
        const valor = parseFloat(inputPrecio.value);
        if (isNaN(valor) || valor <= 0) {
            showError(inputPrecio, "Ingrese un precio válido mayor a Q0.00.");
        } else if (valor < 100) {
            showError(inputPrecio, "El precio mínimo permitido es Q100.00.");
        } else if (valor > 100000000) {
            showError(inputPrecio, "El precio no puede exceder Q100,000,000.00.");
        } else {
            markValid(inputPrecio);
            clearError(inputPrecio);
        }
    });

    inputFecha.addEventListener("change", () => {
        const fecha = new Date(inputFecha.value);
        const hoy = new Date();
        if (!inputFecha.value) {
            showError(inputFecha, "Debe ingresar la fecha de adquisición.");
        } else if (fecha > hoy) {
            showError(inputFecha, "La fecha de adquisición no puede ser futura.");
        } else {
            markValid(inputFecha);
            clearError(inputFecha);
        }
    });

    selectTipoInmueble.addEventListener("change", () => {
        if (selectTipoInmueble.value === "") {
            showError(selectTipoInmueble, "Debe seleccionar un tipo de inmueble.");
        } else {
            markValid(selectTipoInmueble);
            clearError(selectTipoInmueble);
        }
    });

    selectTipoPropiedad.addEventListener("change", () => {
        if (selectTipoPropiedad.value === "") {
            showError(selectTipoPropiedad, "Debe seleccionar un tipo de propiedad.");
        } else {
            markValid(selectTipoPropiedad);
            clearError(selectTipoPropiedad);
        }
    });

    // ===============================
    // VALIDACIÓN FINAL (SUBMIT)
    // ===============================
    form.addEventListener("submit", function (event) {
        event.preventDefault();
        let errores = [];

        // --- Nombre del bien ---
        const nombre = inputNombre.value.trim();
        if (nombre === "" || nombre.length < 3) {
            errores.push("Debe ingresar un nombre válido para el bien (mínimo 3 caracteres).");
            showError(inputNombre, "Debe ingresar un nombre válido para el bien.");
        }

        // --- Fecha de adquisición ---
        const fecha = new Date(inputFecha.value);
        if (!inputFecha.value) {
            errores.push("Debe ingresar la fecha de adquisición.");
            showError(inputFecha, "Debe ingresar la fecha de adquisición.");
        } else if (fecha > new Date()) {
            errores.push("La fecha de adquisición no puede ser futura.");
            showError(inputFecha, "La fecha de adquisición no puede ser futura.");
        }

        // --- Descripción ---
        const descripcion = inputDescripcion.value.trim();
        if (descripcion === "" || descripcion.length < 10) {
            errores.push("Debe ingresar una descripción válida (mínimo 10 caracteres).");
            showError(inputDescripcion, "Debe ingresar una descripción válida (mínimo 10 caracteres).");
        }

        // --- Forma de adquisición ---
        const forma = inputForma.value.trim();
        if (forma === "") {
            errores.push("Debe indicar la forma de adquisición.");
            showError(inputForma, "Debe indicar la forma de adquisición.");
        }

        // --- Precio ---
        const precio = parseFloat(inputPrecio.value);
        if (isNaN(precio) || precio <= 0) {
            errores.push("Debe ingresar un precio válido mayor a Q0.00.");
            showError(inputPrecio, "Debe ingresar un precio válido mayor a Q0.00.");
        } else if (precio > 100000000) {
            errores.push("El precio no puede exceder Q100,000,000.00.");
            showError(inputPrecio, "El precio no puede exceder Q100,000,000.00.");
        }

        // --- Tipo de inmueble ---
        if (selectTipoInmueble.value === "") {
            errores.push("Debe seleccionar un tipo de inmueble.");
            showError(selectTipoInmueble, "Debe seleccionar un tipo de inmueble.");
        }

        // --- Tipo de propiedad ---
        if (selectTipoPropiedad.value === "") {
            errores.push("Debe seleccionar un tipo de propiedad.");
            showError(selectTipoPropiedad, "Debe seleccionar un tipo de propiedad.");
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

        // Enviar tras breve pausa
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
