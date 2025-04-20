import type { IAvis } from '@/domain/entities/Avis';

/**
 * Calcule la moyenne des notes des avis.
 * @param avis - Liste des avis.
 * @returns La moyenne des notes arrondie à 1 décimale ou 0 si aucun avis.
 */
export function calculateAverageRating(avis: IAvis[]): number {
    if (!avis || avis.length === 0) return 0; // Pas d'avis
    const total = avis.reduce((sum, avis) => sum + avis.noteAvis, 0);
    return parseFloat((total / avis.length).toFixed(1)); // Moyenne arrondie à 1 décimale
}

/**
 * Retourne le nombre total d'avis.
 * @param avis - Liste des avis.
 * @returns Le nombre total d'avis.
 */
export function getTotalReviews(avis: IAvis[]): number {
    return avis ? avis.length : 0;
}

/**
 * Retourne la répartition des notes (par exemple, combien de 5 étoiles, 4 étoiles, etc.).
 * @param avis - Liste des avis.
 * @returns Un objet contenant la répartition des notes.
 */
export function getRatingDistribution(avis: IAvis[]): Record<number, number> {
    const distribution: Record<number, number> = { 1: 0, 2: 0, 3: 0, 4: 0, 5: 0 };
    if (!avis || avis.length === 0) return distribution;

    avis.forEach((avis) => {
        if (avis.noteAvis >= 1 && avis.noteAvis <= 5) {
            distribution[avis.noteAvis]++;
        }
    });

    return distribution;
}