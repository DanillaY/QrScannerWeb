async function postData(url = "", data = {}) {
    const response = await fetch(url,
        {
            method: "POST",
            body: JSON.stringify(data)
        });
    return response.json();
}


setInterval(() => {

    postData("http://192.168.1.152:8000/api/Qr/NewQr", { answer: 42 }).then((data) => {

        document.getElementById("ContentEncode").textContent = data["contentEncode"];
        document.getElementById("QrCode").src = "data:image/png;base64," + data["image"];
    });
}, 10000)