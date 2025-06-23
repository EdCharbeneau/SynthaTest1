window.setSource = (elementId, bytes, contentType, title) => {
    // Convert the .NET byte[] to a Uint8Array
    const uint8Array = new Uint8Array(bytes);               
    let blobOptions = {};
    if (contentType) {
        blobOptions['type'] = contentType;
    }
    const blob = new Blob([uint8Array], blobOptions);
    const url = URL.createObjectURL(blob);
    const element = document.getElementById(elementId);
    if (element) {
        element.title = title;
        element.src = url;
    } else {
        console.warn('Element not found:', elementId);
    }
}