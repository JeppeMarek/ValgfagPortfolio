export function enableTabIndent(element) {
    if (!element) return;
    
    element.addEventListener('keydown', (e) => {
        if (e.key === 'Tab') {
            e.preventDefault();
            const start = element.selectionStart;
            const end = element.selectionEnd;
            
            element.value = element.value.substring(0, start) + '\t' + element.value.substring(end);
            element.selectionStart = element.selectionEnd = start + 1;
            
            element.dispatchEvent(new Event('change', { bubbles: true }));
        }
    });
}
