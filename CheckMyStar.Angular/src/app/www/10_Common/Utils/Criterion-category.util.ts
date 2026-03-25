export interface CriterionCategory {
  category: string;
  section: string;
}

const CRITERION_CATEGORIES: { from: number; to: number; category: string; section: string }[] = [
  { from: 1,   to: 2,   category: 'Surfaces de l\'habitation',                                                    section: '1.1 Aménagement général' },
  { from: 3,   to: 4,   category: 'Équipement électrique de l\'habitation',                                       section: '1.1 Aménagement général' },
  { from: 5,   to: 7,   category: 'Téléphonie et communication',                                                  section: '1.1 Aménagement général' },
  { from: 8,   to: 13,  category: 'Télévision et équipement hi-fi',                                               section: '1.1 Aménagement général' },
  { from: 14,  to: 22,  category: 'Équipements pour le confort du client',                                        section: '1.1 Aménagement général' },
  { from: 23,  to: 27,  category: 'Mobiliers',                                                                    section: '1.1 Aménagement général' },
  { from: 28,  to: 36,  category: 'Aménagement des chambres (literie, équipements)',                              section: '1.2 Aménagement des chambres' },
  { from: 37,  to: 55,  category: 'Équipements et aménagement des sanitaires',                                    section: '1.3 Équipements et aménagement des sanitaires' },
  { from: 56,  to: 75,  category: 'Équipements et aménagement de la cuisine ou du coin cuisine',                  section: '1.4 Équipements et aménagement de la cuisine' },
  { from: 76,  to: 94,  category: 'Environnement et extérieurs (parking, balcon, jardin, loisirs, vue, etc.)',    section: '1.5 Environnement et extérieurs' },
  { from: 95,  to: 99,  category: 'État et propreté des installations et des équipements',                        section: '1.6 État et propreté' },
  { from: 100, to: 115, category: 'Services aux clients',                                                         section: 'Chapitre 2 – Services aux clients' },
  { from: 116, to: 122, category: 'Accessibilité',                                                                section: '3.1 Accessibilité' },
  { from: 123, to: 133, category: 'Développement durable',                                                        section: '3.2 Développement durable' },
];

/**
 * Returns the category info for a given criterionId, or null if not found.
 */
export function getCriterionCategory(criterionId: number): CriterionCategory | null {
  const range = CRITERION_CATEGORIES.find(r => criterionId >= r.from && criterionId <= r.to);
  return range ? { category: range.category, section: range.section } : null;
}

/**
 * Returns the first criterionId of each category (used to detect category boundaries).
 */
export const CATEGORY_BOUNDARY_IDS = new Set(CRITERION_CATEGORIES.map(r => r.from));
