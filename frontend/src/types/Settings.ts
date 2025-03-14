export type Color = {
  name: string
  images: string
}

export type Fabric = {
  name: string
  colors: Color[]
} 

export type Settings = {
  fabrics: Fabric[]
}