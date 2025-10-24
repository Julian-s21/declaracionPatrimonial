document.addEventListener("DOMContentLoaded", () => {
    const form = document.getElementById("loginForm");
    const correo = document.getElementById("Correo_Usuario");
    const pass = document.getElementById("Contrasena_Usuario");
    const correoError = document.getElementById("CorreoError");
    const passError = document.getElementById("PassError");

    
    // Lista de contraseñas inseguras o comunes
    const contrasenasInseguras = [
        "123456", "password", "qwerty", "abc123", "contraseña", "admin", "usuario",
        "12345678", "111111", "123123", "000000", "iloveyou", "1234", "welcome", "letmein"
    ];

    // Dominios de correo poco confiables o temporales
    const dominiosProhibidos = [
        "tempmail.com", "mailinator.com", "guerrillamail.com", "10minutemail.com",
        "yopmail.com", "trashmail.com", "fakeinbox.com"
    ];

    form.addEventListener("submit", function (e) {
        e.preventDefault();

        let errores = [];

        // Limpiar estados previos
        [correo, pass].forEach(i => i.classList.remove("is-invalid"));
        [correoError, passError].forEach(i => i.textContent = "");

        // -----------------------
        // VALIDACIONES DE CORREO
        // -----------------------
        const correoVal = correo.value.trim();

        if (!correoVal) {
            errores.push("El campo 'Correo Electronico' es obligatorio.");
            correoError.textContent = "Por favor ingresa tu correo electronico.";
            correo.classList.add("is-invalid");
        } else {
            const [parteUsuario, parteDominio] = correoVal.split("@");

            if (!parteDominio) {
                errores.push("El correo debe contener un símbolo '@'.");
                correoError.textContent = "Falta el dominio del correo.";
                correo.classList.add("is-invalid");
            } else if (correoVal.length < 6) {
                errores.push("El correo es demasiado corto.");
                correoError.textContent = "Debe tener al menos 6 caracteres.";
                correo.classList.add("is-invalid");
            } else if (correoVal.length > 100) {
                errores.push("El correo no puede exceder los 100 caracteres.");
                correoError.textContent = "Correo demasiado largo.";
                correo.classList.add("is-invalid");
            } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(correoVal)) {
                errores.push("El formato del correo es incorrecto (ej. nombre@dominio.com).");
                correoError.textContent = "Formato invalido de correo.";
                correo.classList.add("is-invalid");
            } else if (/\s/.test(correoVal)) {
                errores.push("El correo no debe contener espacios en blanco.");
                correoError.textContent = "No se permiten espacios.";
                correo.classList.add("is-invalid");
            } else if (/[^a-zA-Z0-9@._-]/.test(correoVal)) {
                errores.push("El correo contiene caracteres no permitidos.");
                correoError.textContent = "Solo se permiten letras, numeros y los simbolos @ . _ -";
                correo.classList.add("is-invalid");
            } else if (!parteDominio.includes(".")) {
                errores.push("El dominio del correo es incompleto (ej. falta .com o .org).");
                correoError.textContent = "Dominio incompleto.";
                correo.classList.add("is-invalid");
            } else if (dominiosProhibidos.some(d => correoVal.toLowerCase().endsWith(d))) {
                errores.push("No se permite el uso de correos temporales o poco confiables.");
                correoError.textContent = "Usa un correo valido.";
                correo.classList.add("is-invalid");
            }
        }

        // ---------------------------
        // VALIDACIONES DE CONTRASENA
        // ---------------------------
        const passVal = pass.value;

        if (!passVal.trim()) {
            errores.push("El campo 'Contrasena' es obligatorio.");
            passError.textContent = "Por favor ingresa tu contrasena.";
            pass.classList.add("is-invalid");
        } else {
            if (passVal.length < 6) {
                errores.push("La contrasena debe tener al menos 6 caracteres.");
                passError.textContent = "Muy corta.";
                pass.classList.add("is-invalid");
            } else if (passVal.length > 50) {
                errores.push("La contrasena no puede tener mas de 50 caracteres.");
                passError.textContent = "Demasiado larga.";
                pass.classList.add("is-invalid");
            } else if (/\s/.test(passVal)) {
                errores.push("La contrasena no debe contener espacios.");
                passError.textContent = "No se permiten espacios.";
                pass.classList.add("is-invalid");
            } else {
                if (!/[A-Za-z]/.test(passVal)) errores.push("Debe contener al menos una letra.");
                if (!/[0-9]/.test(passVal)) errores.push("Debe incluir al menos un numero.");
                if (!/[!@#$%^&*(),.?":{}|<>_\-]/.test(passVal)) errores.push("Debe incluir un caracter especial.");
                if (passVal.toLowerCase() === passVal || passVal.toUpperCase() === passVal)
                    errores.push("Combina mayusculas y minusculas.");
                if (/^[A-Za-z]+$/.test(passVal))
                    errores.push("Tu contrasena solo tiene letras. Agrega numeros o simbolos.");
                if (/^[0-9]+$/.test(passVal))
                    errores.push("Tu contrasena solo tiene numeros. Agrega letras o simbolos.");
                if (/(.)\1{2,}/.test(passVal))
                    errores.push("Evita repetir el mismo caracter mas de 2 veces seguidas.");
                if (contrasenasInseguras.includes(passVal.toLowerCase())) {
                    errores.push("La contrasena ingresada es demasiado comun o insegura.");
                    passError.textContent = "Contrasena insegura.";
                    pass.classList.add("is-invalid");
                }
            }
        }

        // ---------------------------
        // MOSTRAR RESULTADO
        // ---------------------------
        if (errores.length > 0) {
            Swal.fire({
                icon: 'error',
                title: 'Errores de validacion',
                html: errores.map(e => `<p class='mb-1'>❌ ${e}</p>`).join(''),
                confirmButtonColor: '#a910bd',
                width: 500,
                scrollbarPadding: false
            });
            return;
        }

        // Confirmacion visual antes de enviar
        Swal.fire({
            title: 'Iniciando sesion...',
            text: 'Por favor espera mientras validamos tus datos.',
            icon: 'info',
            showConfirmButton: false,
            timer: 1500,
            timerProgressBar: true
        }).then(() => {
            form.submit();
        });
    });

    // ---------------------------
    // VALIDACION EN TIEMPO REAL
    // ---------------------------
    correo.addEventListener("input", () => {
        const val = correo.value.trim();
        if (/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(val) && val.length > 5) {
            correo.classList.remove("is-invalid");
            correoError.textContent = "";
        }
    });

    pass.addEventListener("input", () => {
        const val = pass.value.trim();
        if (val.length >= 6 && !/\s/.test(val)) {
            pass.classList.remove("is-invalid");
            passError.textContent = "";
        }
    });
});
