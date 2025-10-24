document.addEventListener("DOMContentLoaded", () => {
    const form = document.getElementById("registerForm");

    const nombre = document.getElementById("Nombre_Usuario");
    const apellido = document.getElementById("Apellido_Usuario");
    const correo = document.getElementById("Correo_Usuario");
    const pass = document.getElementById("Contrasena_Usuario");
    const confirmPass = document.getElementById("Confirmar_Contrasena");

    const nombreError = document.getElementById("NombreError");
    const apellidoError = document.getElementById("ApellidoError");
    const correoError = document.getElementById("CorreoError");
    const passError = document.getElementById("PassError");
    const confirmPassError = document.getElementById("ConfirmPassError");

    const contrasenasInseguras = [
        "123456","password","qwerty","abc123","contraseña","admin","usuario",
        "12345678","111111","123123","000000","iloveyou","1234","welcome","letmein"
    ];

    const dominiosProhibidos = [
        "tempmail.com","mailinator.com","guerrillamail.com","10minutemail.com",
        "yopmail.com","trashmail.com","fakeinbox.com"
    ];

    // Función para evaluar fuerza de contraseña
    const evaluarFuerza = (val) => {
        let score = 0;
        if (val.length >= 6) score += 1;
        if (val.length >= 10) score += 1;
        if (/[A-Z]/.test(val)) score += 1;
        if (/[a-z]/.test(val)) score += 1;
        if (/[0-9]/.test(val)) score += 1;
        if (/[!@#$%^&*(),.?":{}|<>_\-]/.test(val)) score += 1;
        return score;
    };

    const actualizarBarraFuerza = (val) => {
        const barra = document.getElementById("passStrength");
        const texto = document.getElementById("strengthText");
        const score = evaluarFuerza(val);

        barra.style.width = `${(score / 6) * 100}%`;

        if (score <= 2) {
            barra.className = "progress-bar bg-danger";
            texto.textContent = "Muy débil";
        } else if (score <= 4) {
            barra.className = "progress-bar bg-warning";
            texto.textContent = "Media";
        } else {
            barra.className = "progress-bar bg-success";
            texto.textContent = "Fuerte";
        }
    };

    form.addEventListener("submit", function (e) {
        e.preventDefault();
        let errores = [];

        // Limpiar errores previos
        [nombre, apellido, correo, pass, confirmPass].forEach(i => i.classList.remove("is-invalid"));
        [nombreError, apellidoError, correoError, passError, confirmPassError].forEach(i => i.textContent = "");

        // -----------------------
        // VALIDACIONES NOMBRE
        // -----------------------
        const nombreVal = nombre.value.trim();
        if (!nombreVal) {
            errores.push("El nombre es obligatorio.");
            nombreError.textContent = "Por favor ingresa tu nombre.";
            nombre.classList.add("is-invalid");
        } else if (nombreVal.length < 2) {
            errores.push("El nombre debe tener al menos 2 caracteres.");
            nombreError.textContent = "Muy corto.";
            nombre.classList.add("is-invalid");
        } else if (nombreVal.length > 50) {
            errores.push("El nombre es demasiado largo.");
            nombreError.textContent = "Demasiado largo.";
            nombre.classList.add("is-invalid");
        } else if (/[^a-zA-ZáéíóúÁÉÍÓÚñÑ\s]/.test(nombreVal)) {
            errores.push("El nombre contiene caracteres no permitidos.");
            nombreError.textContent = "Solo letras y espacios.";
            nombre.classList.add("is-invalid");
        }

        // -----------------------
        // VALIDACIONES APELLIDO
        // -----------------------
        const apellidoVal = apellido.value.trim();
        if (!apellidoVal) {
            errores.push("El apellido es obligatorio.");
            apellidoError.textContent = "Por favor ingresa tu apellido.";
            apellido.classList.add("is-invalid");
        } else if (apellidoVal.length < 2) {
            errores.push("El apellido debe tener al menos 2 caracteres.");
            apellidoError.textContent = "Muy corto.";
            apellido.classList.add("is-invalid");
        } else if (apellidoVal.length > 50) {
            errores.push("El apellido es demasiado largo.");
            apellidoError.textContent = "Demasiado largo.";
            apellido.classList.add("is-invalid");
        } else if (/[^a-zA-ZáéíóúÁÉÍÓÚñÑ\s]/.test(apellidoVal)) {
            errores.push("El apellido contiene caracteres no permitidos.");
            apellidoError.textContent = "Solo letras y espacios.";
            apellido.classList.add("is-invalid");
        }

        // -----------------------
        // VALIDACIONES CORREO
        // -----------------------
        const correoVal = correo.value.trim();
        if (!correoVal) {
            errores.push("El correo es obligatorio.");
            correoError.textContent = "Por favor ingresa tu correo.";
            correo.classList.add("is-invalid");
        } else {
            const [parteUsuario, parteDominio] = correoVal.split("@");
            if (!parteDominio || !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(correoVal)) {
                errores.push("Formato de correo inválido.");
                correoError.textContent = "Correo inválido.";
                correo.classList.add("is-invalid");
            } else if (dominiosProhibidos.some(d => correoVal.toLowerCase().endsWith(d))) {
                errores.push("No se permiten correos temporales.");
                correoError.textContent = "Usa un correo válido.";
                correo.classList.add("is-invalid");
            }
        }

        // -----------------------
        // VALIDACIONES CONTRASEÑA
        // -----------------------
        const passVal = pass.value;
        if (!passVal.trim()) {
            errores.push("La contraseña es obligatoria.");
            passError.textContent = "Por favor ingresa tu contraseña.";
            pass.classList.add("is-invalid");
        } else {
            if (passVal.length < 6) {
                errores.push("La contraseña debe tener al menos 6 caracteres.");
                passError.textContent = "Muy corta.";
                pass.classList.add("is-invalid");
            } else if (passVal.length > 50) {
                errores.push("La contraseña es demasiado larga.");
                passError.textContent = "Demasiado larga.";
                pass.classList.add("is-invalid");
            } else {
                if (!/[A-Za-z]/.test(passVal)) errores.push("Debe contener al menos una letra.");
                if (!/[0-9]/.test(passVal)) errores.push("Debe incluir al menos un número.");
                if (!/[!@#$%^&*(),.?":{}|<>_\-]/.test(passVal)) errores.push("Debe incluir un carácter especial.");
                if (/^[A-Za-z]+$/.test(passVal)) errores.push("Solo letras. Agrega números o símbolos.");
                if (/^[0-9]+$/.test(passVal)) errores.push("Solo números. Agrega letras o símbolos.");
                if (/(\S)\1{2,}/.test(passVal)) errores.push("Evita repetir el mismo caracter más de 2 veces seguidas.");
                if (contrasenasInseguras.includes(passVal.toLowerCase())) errores.push("Contraseña demasiado común.");
            }
        }

        // -----------------------
        // VALIDACIONES CONFIRMAR CONTRASEÑA
        // -----------------------
        const confirmVal = confirmPass.value;
        if (!confirmVal.trim()) {
            errores.push("Debes confirmar la contraseña.");
            confirmPassError.textContent = "Campo obligatorio.";
            confirmPass.classList.add("is-invalid");
        } else if (passVal !== confirmVal) {
            errores.push("Las contraseñas no coinciden.");
            confirmPassError.textContent = "No coinciden.";
            confirmPass.classList.add("is-invalid");
        }

        actualizarBarraFuerza(passVal);

        // -----------------------
        // MOSTRAR ERRORES
        // -----------------------
        if (errores.length > 0) {
            Swal.fire({
                icon: 'error',
                title: 'Errores de validación',
                html: errores.map(e => `<p class='mb-1'>❌ ${e}</p>`).join(''),
                confirmButtonColor: '#a910bd',
                width: 500,
                scrollbarPadding: false
            });
            return;
        }

        Swal.fire({
            title: 'Registrando usuario...',
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
    [nombre, apellido, correo, pass, confirmPass].forEach(input => {
        input.addEventListener("input", () => {
            input.classList.remove("is-invalid");
            const errorSpan = document.getElementById(input.id + "Error");
            if (errorSpan) errorSpan.textContent = "";
        });
    });

    pass.addEventListener("input", () => actualizarBarraFuerza(pass.value));
});
