export interface Product {
  partSKU: string;
  band: number | null;
  categoryCode: string | null;
  manufacturer: string | null;
  itemDescription: string | null;
  listPrice: number | null;
  minimumDiscount: number | null;
  discountedPrice: number | null;
}