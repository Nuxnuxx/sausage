import { useNotification } from '@kyvg/vue3-notification';

export function validateForm(formData: Record<string, any>, requiredFields: { field: string; label: string }[]) {
  const { notify } = useNotification();

  const missingFields = requiredFields.filter(({ field }) => !formData[field]);

  if (missingFields.length > 0) {
    missingFields.forEach(({ label }) => {
      notify({
        type: 'error',
        title: 'Champ requis',
        text: `Le champ "${label}" est obligatoire.`,
      });
    });
    return false; // Validation échouée
  }

  return true; // Validation réussie
}