<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:output method="html" indent="yes" encoding="UTF-8"/>

<xsl:template match="/">
    <html>
        <head>
            <title>Reservations</title>
        </head>
        <body>
            <h1>Reservations</h1>
            <table border="1">
                <thead>
                    <tr>
                        <th>Number</th>
                        <th>Customer Name</th>
                        <th>Car Model</th>
                        <th>Pickup Date</th>
                        <th>Return Date</th>
                        <th>Total Cost</th>
                    </tr>
                </thead>
                <tbody>
                    <xsl:apply-templates select="reservation_info/rezervare"/>
                </tbody>
            </table>
        </body>
    </html>
</xsl:template>

<xsl:template match="rezervare">
    <tr>
        <td><xsl:value-of select="@numar"/></td>
        <td><xsl:value-of select="customer/name"/></td>
        <td><xsl:value-of select="car/model"/></td>
        <td><xsl:value-of select="pickup_date"/></td>
        <td><xsl:value-of select="return_date"/></td>
        <td><xsl:value-of select="payment/total_cost"/></td>
    </tr>
</xsl:template>

</xsl:stylesheet>
